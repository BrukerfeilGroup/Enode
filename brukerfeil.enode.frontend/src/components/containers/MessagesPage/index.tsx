import React, { useState, useEffect } from 'react'
import { match } from 'react-router-dom'
import Message from '../../../types/Message'
import useMessages from '../../../hooks/useMessages'
import InOrOutbox from '../../common/InOrOutbox'
import Navbar from '../../common/Navbar'
import MessageModal from '../../MessageModal'
import { filterMessages, initialFilter, checkboxStructure } from '../../../utils/utils'
import { MESSAGES_IN_ENDPOINT, MESSAGES_OUT_ENDPOINT } from '../../../constants'
import HamburgerMenu from '../../HamburgerMenu'
import FilterBox, { Filters, CheckboxStructure } from '../../containers/Filter'
import styles from './styles.module.css'

type MessagesPageRouteParams = {
    id: string
}

export type MessagesPageProps = {
    match: match<MessagesPageRouteParams>
}

const MessagesPage: React.FC<MessagesPageProps> = props => {
    //orgId from browers URL
    const orgId = props.match.params.id

    const [inMessages, setInMessages] = useState<Message[]>([]) //State for incoming messages
    const [outMessages, setOutMessages] = useState<Message[]>([]) //State for
    const [searchMessage, setSearchMessage] = useState<Message>()
    const [searchedMessage, setSearchedMessage] = useState<string>('')
    const [activeMessage, setActiveMessage] = useState<Message>() //State for a selected message
    const [filteredSenderOrg, setFilteredSenderOrg] = useState<string>('')
    const [filteredReceiverOrg, setFilteredReceiverOrg] = useState<string>('')
    const [isHamburgerOpen, setIsHamburgerOpen] = useState<boolean>(false)
    const [isFilterOpen, setIsFilterOpen] = useState<boolean>(false)
    const [filters, setFilters] = useState<Filters>(initialFilter)
    const [checkboxes, setCheckboxes] = useState<CheckboxStructure>(checkboxStructure)

    //All objects and functions regarding messages
    const {
        tempInMessages,
        tempOutMessages,
        isFetching,
        fetchBySenderId,
        fetchByReceiverId,
        fetchMessages,
    } = useMessages(orgId)

    const handleClearIncomingMessages = () => {
        setFilteredSenderOrg('')
        fetchMessages(MESSAGES_IN_ENDPOINT)
    }

    const handleClearOutgoingMessages = () => {
        setFilteredReceiverOrg('')
        fetchMessages(MESSAGES_OUT_ENDPOINT)
    }

    const handleSearchOutgoingMessages = (orgId: string) => {
        fetchByReceiverId(orgId)
        setFilteredReceiverOrg(orgId)
    }

    const handleSearchIngoingMessages = (orgId: string) => {
        fetchBySenderId(orgId)
        setFilteredSenderOrg(orgId)
    }

    const findMessage = (id: string) => {
        const message = [...inMessages, ...outMessages].find(m =>
            m.difiMessage
                ? m.difiMessage.messageId === id
                : m.elementsMessage.conversationId === id
        )

        if (message) setActiveMessage(message)
    }

    const handleSearchMessageId = (messageId: string) => {
        const message = [...inMessages, ...outMessages].find(m =>
            m.difiMessage
                ? m.difiMessage.messageId === messageId
                : m.elementsMessage.conversationId === messageId
        )

        if (message) setSearchMessage(message)
    }

    const handleClearMessageId = () => {
        setSearchedMessage('')
    }

    const handleCloseModal = () => {
        setSearchMessage(undefined)
    }

    const handleFilterChange = (filters: Filters) => {
        setInMessages(filterMessages(tempInMessages, filters))
        setOutMessages(filterMessages(tempOutMessages, filters))
        populatedFilters(filters)
    }

    const handleCheckboxChange = (checkboxStructure: CheckboxStructure) => {
        populatedCheckboxes(checkboxStructure)
    }

    useEffect(() => {
        setInMessages(tempInMessages)
        setOutMessages(tempOutMessages)
    }, [tempInMessages, tempOutMessages])

    const populatedFilters = (initialFilter: Filters) => {
        setFilters(initialFilter)
    }

    const populatedCheckboxes = (checkboxStructure: CheckboxStructure) => {
        setCheckboxes(checkboxStructure)
    }
    return (
        <>
            <Navbar
                orgId={orgId}
                toggleHamburger={() => setIsHamburgerOpen(!isHamburgerOpen)}
                toggleFilter={() => setIsFilterOpen(!isFilterOpen)}
                onSearch={handleSearchMessageId}
                onClear={handleClearMessageId}
                onClose={handleCloseModal}
                filteredOrgId={searchedMessage}
                message={searchMessage}
            />
            {isHamburgerOpen === true ? <HamburgerMenu org={orgId} /> : null}

            {isFilterOpen === true ? (
                <FilterBox
                    checkboxStructure={checkboxes}
                    onCheckboxChange={checkboxStructure =>
                        handleCheckboxChange(checkboxStructure)
                    }
                    initialFilter={filters}
                    onFilterChange={initialFilter => handleFilterChange(initialFilter)}
                />
            ) : null}
            <div className={styles.container}>
                {isFetching ? (
                    <pre>Loading..</pre>
                ) : (
                    <>
                        <>
                            <InOrOutbox
                                direction="IN"
                                messages={inMessages}
                                filteredOrgId={filteredSenderOrg}
                                onChangeActive={findMessage}
                                onSearch={handleSearchIngoingMessages}
                                onClearFilteredMessages={handleClearIncomingMessages}
                            />
                            <InOrOutbox
                                direction="OUT"
                                messages={outMessages}
                                filteredOrgId={filteredReceiverOrg}
                                onChangeActive={findMessage}
                                onSearch={handleSearchOutgoingMessages}
                                onClearFilteredMessages={handleClearOutgoingMessages}
                            />
                        </>

                        {activeMessage ? (
                            <MessageModal
                                message={activeMessage}
                                onCloseModal={() => setActiveMessage(undefined)}
                            />
                        ) : null}
                    </>
                )}
            </div>
        </>
    )
}
export default MessagesPage
