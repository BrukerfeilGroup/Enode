import React, { useState, useEffect } from 'react'
import Message from '../../../types/Message'
import SearchSenderReceiver from '../../containers/SearchSenderReceiver'
import IncomingMessagesTable from './IncomingMessagesTable'
import OutgoingMessagesTable from './OutgoingMessagesTable'
import SearchMoreRow from './SearchMoreRow'
import styles from './styles.module.css'

type InOrOutboxProps = {
    direction: 'IN' | 'OUT'
    messages: Message[]
    filteredOrgId?: string
    onChangeActive: (messageId: string) => void
    onSearch: (id: string) => void
    onClearFilteredMessages: () => void
}

const InOrOutbox: React.FC<InOrOutboxProps> = props => {
    const [messages, setMessages] = useState<Message[]>(props.messages)
    const [orgIdToBeSearched, setOrgIdToBeSearched] = useState<string>('')

    const dirCheck = props.direction === 'IN'

    const filterMessages = (input: string) => {
        if (input.length === 9) {
            setOrgIdToBeSearched(input)
            return
        }
        //Don't filter anything if user has already clicked search
        else if (input.length === 0 || props.filteredOrgId) {
            if (!props.filteredOrgId) {
                //Skipping filtering if there is no input AND the user has NOT yet searched
                setMessages(props.messages)
            } else {
                //Refresh message list to unfiltered version if user changes input field after having searched
                props.onClearFilteredMessages()
            }
            setOrgIdToBeSearched('')
            return
        }

        const filtered = props.messages.filter(p => {
            if (!dirCheck) {
                if (p.elementsMessage.externalId === '' || null) {
                    return false
                }
            }
            const regExp = new RegExp(`(${input})`, 'gi')
            return dirCheck
                ? regExp.test(p.difiMessage.senderIdentifier.toString())
                : regExp.test(p.elementsMessage.externalId)
        })

        setMessages(filtered)
        setOrgIdToBeSearched(input)
    }

    useEffect(() => {
        setMessages(props.messages)
    }, [props.messages])

    useEffect(() => {
        if (props.filteredOrgId) setOrgIdToBeSearched(props.filteredOrgId)
    }, [props])

    return (
        <div className={styles.tableContainer}>
            <div className={styles.titleContainer}>
                <h2 className={styles.title}>{dirCheck ? 'Innkommende' : 'Utgående'}</h2>
                <SearchSenderReceiver
                    placeholder={dirCheck ? 'Søk på avsender' : 'Søk på mottaker'}
                    onSearch={id => props.onSearch(id)}
                    onChange={filterMessages}
                    onClear={props.onClearFilteredMessages}
                    filteredOrgId={props.filteredOrgId}
                />
            </div>

            {dirCheck ? (
                <IncomingMessagesTable
                    messages={messages}
                    onChangeActive={props.onChangeActive}
                >
                    {orgIdToBeSearched.length === 9 ? (
                        <SearchMoreRow
                            rowSpan={6}
                            onClick={props.onSearch}
                            orgIdToBeSearched={orgIdToBeSearched}
                        />
                    ) : null}
                </IncomingMessagesTable>
            ) : (
                <OutgoingMessagesTable
                    messages={messages}
                    onChangeActive={props.onChangeActive}
                >
                    {orgIdToBeSearched.length === 9 ? (
                        <SearchMoreRow
                            rowSpan={7}
                            onClick={props.onSearch}
                            orgIdToBeSearched={orgIdToBeSearched}
                        />
                    ) : null}
                </OutgoingMessagesTable>
            )}
        </div>
    )
}

export default InOrOutbox
