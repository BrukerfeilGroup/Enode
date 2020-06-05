import React, { useState, useEffect } from 'react'
import { Redirect } from 'react-router-dom'
import Message from '../../../types/Message'
import useMessages from '../../../hooks/useMessages'
import MessageTableContainer from '../MessageTableContainer'
import Navbar from '../../common/Navbar'
import MessageModal from '../../MessageModal'
import { filterMessages, initialFilter, checkboxStructure } from '../../../utils/utils'
import { MESSAGES_IN_ENDPOINT, MESSAGES_OUT_ENDPOINT } from '../../../constants'
import HamburgerMenu from '../../common/HamburgerMenu'
import FilterBox, { Filters, CheckboxStructure } from '../../containers/Filter'
import { useParams } from 'react-router-dom'
import styles from './styles.module.css'

const MessagesPage: React.FC = () => {
    //orgId from browers URL
    const { orgId } = useParams<{ orgId: string }>()
    const [inMessages, setInMessages] = useState<Message[]>([]) // State for incoming messages - may be filtered
    const [outMessages, setOutMessages] = useState<Message[]>([]) // State for outgoing messages - may be filtered
    const [activeMessage, setActiveMessage] = useState<Message>() // State for a selected message
    const [filteredSenderOrg, setFilteredSenderOrg] = useState<string>('') // The orgId a user has filtered incoming messages on
    const [filteredReceiverOrg, setFilteredReceiverOrg] = useState<string>('') // The orgId a user has filtered outgoing messages on
    const [isHamburgerOpen, setIsHamburgerOpen] = useState<boolean>(false)
    const [conversationIdFilteredBy, setConversationIdFilteredBy] = useState<string>('') // The id which user has inputted in the search field in navbar
    const [isFilterOpen, setIsFilterOpen] = useState<boolean>(false)
    const [filters, setFilters] = useState<Filters>(initialFilter)
    const [checkboxes, setCheckboxes] = useState<CheckboxStructure>(checkboxStructure) // Object representing which checkboxes are checked (in filters)

    //All objects and functions regarding messages
    const {
        tempInMessages, // The messages directly from API, before any frontend filtering/sorting
        tempOutMessages,
        searchedMessage,
        setSearchedMessage,
        isFetchingIn,
        isFetchingOut,
        fetchBySenderId,
        fetchByReceiverId,
        fetchMessages,
        fetchByMessageId,
        error,
        setError,
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
        if (message) {
            setError('')
            setActiveMessage(message)
        }
    }

    const handleSearchMessageId = (messageId: string) => {
        setError('')
        setSearchedMessage(null)
        fetchByMessageId(messageId)
    }

    const filterByConversationId = (
        input = conversationIdFilteredBy,
        incomingMessages = tempInMessages,
        outgoingMessages = tempOutMessages
    ) => {
        const filteredIn = incomingMessages.filter(m => {
            const regExp = new RegExp(`(${input})`, 'gi')
            return regExp.test(m.difiMessage.conversationId)
        })

        const filteredOut = outgoingMessages.filter(m => {
            const regExp = new RegExp(`(${input})`, 'gi')
            return regExp.test(m.elementsMessage.conversationId)
        })
        setInMessages(filteredIn)
        setOutMessages(filteredOut)
        setConversationIdFilteredBy(input)
    }

    /**
     * Filters the original list first by the checkbo x filters, THEN filters by a conversationId.
     * This is prevents overriding the conversationId filter
     *
     * @param filters the objects which represents which filters are active
     * @param input a string which represents a conversation id
     */
    const handleFiltering = (filters: Filters, input = conversationIdFilteredBy) => {
        const incomingMessages = filterMessages(tempInMessages, filters)
        const outgoingMessages = filterMessages(tempOutMessages, filters)

        filterByConversationId(input, incomingMessages, outgoingMessages)
        setFilters(filters)
    }

    useEffect(() => {
        if (searchedMessage) setActiveMessage(searchedMessage)
    }, [searchedMessage])

    useEffect(() => {
        setInMessages(tempInMessages)
        setOutMessages(tempOutMessages)
    }, [tempInMessages, tempOutMessages])

    if (error.includes('status code 400')) {
        return <Redirect to="/enode-frontend" />
    }

    return (
        <>
            <Navbar
                filters={true}
                orgId={orgId}
                toggleHamburger={() => setIsHamburgerOpen(!isHamburgerOpen)}
                toggleFilter={() => setIsFilterOpen(!isFilterOpen)}
                onSearch={handleSearchMessageId}
                onFilterMessage={input => handleFiltering(filters, input)}
            />
            {isHamburgerOpen && (
                <HamburgerMenu
                    org={orgId}
                    onCloseHamburger={() => setIsHamburgerOpen(false)}
                />
            )}

            {isFilterOpen && (
                <FilterBox
                    checkboxStructure={checkboxes}
                    onCheckboxChange={setCheckboxes}
                    initialFilter={filters}
                    onFilterChange={handleFiltering}
                    onCloseFilter={() => setIsFilterOpen(false)}
                />
            )}
            {error && (
                <div className={styles.error} onClick={() => setError('')}>
                    {error}
                </div>
            )}
            <div className={styles.container}>
                <MessageTableContainer
                    incoming
                    messages={inMessages}
                    filteredOrgId={filteredSenderOrg}
                    onChangeActive={findMessage}
                    onSearch={handleSearchIngoingMessages}
                    onClearFilteredMessages={handleClearIncomingMessages}
                    isFetching={isFetchingIn}
                />
                <MessageTableContainer
                    incoming={false}
                    messages={outMessages}
                    filteredOrgId={filteredReceiverOrg}
                    onChangeActive={findMessage}
                    onSearch={handleSearchOutgoingMessages}
                    onClearFilteredMessages={handleClearOutgoingMessages}
                    isFetching={isFetchingOut}
                />

                {!error && activeMessage && (
                    <MessageModal
                        message={activeMessage}
                        onCloseModal={() => setActiveMessage(undefined)}
                    />
                )}
            </div>
        </>
    )
}
export default MessagesPage
