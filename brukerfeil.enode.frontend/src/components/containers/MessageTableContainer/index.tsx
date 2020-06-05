import React, { useState, useEffect } from 'react'
import Message from '../../../types/Message'
import MessageTable from '../../common/MessageTable'
import {
    initialDifiSortingState,
    initialElementsSortingState,
} from '../../../constants/sortingStates'
import {
    DifiMessageKeys,
    DifiSortingState,
    ElementsMessageKeys,
    ElementsSortingState,
} from '../../../types/SortingTypes'
import {
    getUpdatedDifiSortingState,
    getUpdatedElementsSortingState,
} from '../../../utils/utils'

export type MessageTableContainerProps = {
    incoming: boolean
    messages: Message[]
    filteredOrgId?: string
    isFetching: boolean
    onChangeActive: (messageId: string) => void
    onSearch: (id: string) => void
    onClearFilteredMessages: () => void
}

export const MessageTableContainer: React.FC<MessageTableContainerProps> = props => {
    const [messages, setMessages] = useState<Message[]>(props.messages)
    const [orgIdToBeSearched, setOrgIdToBeSearched] = useState<string>('')
    // prettier-ignore
    const [difiSortingState, setDifiSortingState] = useState<DifiSortingState>(initialDifiSortingState)
    const [difiColumn, setDifiColumn] = useState<DifiMessageKeys | null>(null)
    const [elementsColumn, setElementsColumn] = useState<ElementsMessageKeys | null>(null)
    // prettier-ignore
    const [elementsSortingState, setElementsSortingState] = useState<ElementsSortingState>(initialElementsSortingState)

    const { incoming } = props

    /**
     * Sorts the incoming messages, but ALSO outgoing if user clicks Difi status column
     *
     * @param column the column which the list should be sorted by
     * @param toggleDirection change the sorting direction
     * @param clearElementsSortingState Clear the elements sorting state when Difi status is clicked. Used to remove arrows on the Elements columns
     */
    const sortDifiMessages = (
        column: DifiMessageKeys,
        toggleDirection: boolean,
        clearElementsSortingState?: boolean
    ) => {
        setDifiColumn(column)
        let sorted = [...props.messages]
        let direction = difiSortingState[column]

        // Don't toggle the sort if the external messages are updated.
        // This is used when we need to refresh the sort, NOT sort by a new direction
        // For instance: When new filters are applied
        if (!toggleDirection) {
            direction =
                direction === 'unsorted'
                    ? 'unsorted'
                    : direction === 'ascending'
                    ? 'descending'
                    : 'ascending'
        }

        sorted.sort((m1, m2) => {
            // Need to check for difiMessage, because it might be null in outgoing messages
            if (!m1.difiMessage || !m2.difiMessage) {
                return direction === 'unsorted' || direction === 'descending' ? -1 : 1
            }

            if (direction === 'unsorted' || direction === 'descending') {
                return m1.difiMessage[column] > m2.difiMessage[column] ? 1 : -1
            } else {
                return m1.difiMessage[column] < m2.difiMessage[column] ? 1 : -1
            }
        })
        setMessages(sorted)
        toggleDirection &&
            setDifiSortingState(
                getUpdatedDifiSortingState(
                    initialDifiSortingState,
                    difiSortingState,
                    column
                )
            )
        clearElementsSortingState && setElementsSortingState(initialElementsSortingState)
    }
    /**
     *  Sorts the elements message list.
     *
     * @param column the column which the list should be sorted by
     * @param toggleDirection change the sorting direction
     */
    const sortElementsMessages = (
        column: ElementsMessageKeys,
        toggleDirection: boolean
    ) => {
        setElementsColumn(column)

        let direction = elementsSortingState[column]
        // Don't toggle the sort if the external messages are updated.
        // This is used when we need to refresh the sort, NOT sort by a new direction
        // For instance: When new filters are applied
        if (!toggleDirection) {
            direction =
                direction === 'unsorted'
                    ? 'unsorted'
                    : direction === 'ascending'
                    ? 'descending'
                    : 'ascending'
        }

        let sorted = [...props.messages]
        sorted.sort((m1, m2) => {
            let el1 =
                column === 'sendingStatus'
                    ? m1.elementsMessage[column]?.description
                    : m1.elementsMessage[column]
            let el2 =
                column === 'sendingStatus'
                    ? m2.elementsMessage[column]?.description
                    : m2.elementsMessage[column]
            if (direction === 'unsorted' || direction === 'descending') {
                return el1 && el2 ? (el1 > el2 ? 1 : -1) : 1
            } else {
                return el1 && el2 ? (el1 < el2 ? 1 : -1) : 1
            }
        })

        setMessages(sorted)
        //Clears the arrow on Difi status
        setDifiSortingState(initialDifiSortingState)
        // Switch sorting direction
        toggleDirection &&
            setElementsSortingState(
                getUpdatedElementsSortingState(
                    initialElementsSortingState,
                    elementsSortingState,
                    column
                )
            )
    }

    /**
     * Filters the message list based a user input
     *
     * @param input a string which should represent a partial or complete organization id.
     */
    const filterMessages = (input: string) => {
        if (input.length === 0) {
            if (!props.filteredOrgId) {
                //Skipping filtering if there is no input AND the user has NOT yet searched
                setMessages(props.messages)
            } else {
                //Refresh message list to unfiltered version if user clears input field AFTER having searched
                props.onClearFilteredMessages()
            }
            setOrgIdToBeSearched('')
            return
        }

        const filtered = props.messages.filter(p => {
            if (!incoming) {
                if (p.elementsMessage.externalId === '' || null) {
                    return false
                }
            }
            const regExp = new RegExp(`(${input})`, 'gi')
            return incoming
                ? regExp.test(p.difiMessage.senderIdentifier.toString())
                : regExp.test(p.elementsMessage.externalId)
        })

        setMessages(filtered)
        setOrgIdToBeSearched(input)
    }

    // Make the sender/recevier filter override the messageId filtering
    // without this, a user can filter on messageid and find messages not visible in the messagelist. This scenario can be reproduced
    // if you filter by a sender/receiver, and then filter by a messageId which you can't see in the messagelist.
    useEffect(() => {
        if (orgIdToBeSearched) {
            filterMessages(orgIdToBeSearched)
        } else {
            if (!difiColumn && !elementsColumn) {
                setMessages(props.messages)
            } else {
                if (difiColumn) {
                    sortDifiMessages(difiColumn, false)
                }
                if (elementsColumn) {
                    sortElementsMessages(elementsColumn, false)
                }
            }
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [props.messages])

    useEffect(() => {
        if (props.filteredOrgId) setOrgIdToBeSearched(props.filteredOrgId)
    }, [props.filteredOrgId])

    return (
        <MessageTable
            {...props}
            messages={messages}
            filteredOrgId={orgIdToBeSearched}
            filterMessages={filterMessages}
            difiSortingStates={difiSortingState}
            onSortDifiMessages={column => sortDifiMessages(column, true, true)}
            elementsSortingStates={elementsSortingState}
            onSortElementsMessages={column => sortElementsMessages(column, true)}
        />
    )
}

export default MessageTableContainer
