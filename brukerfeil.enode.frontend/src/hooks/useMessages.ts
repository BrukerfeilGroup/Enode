import { useState, useEffect } from 'react'
import { httpResponseHandler, httpObjectResponseHandler } from '../utils/utils'
import Message from '../types/Message'
import axios from 'axios'
//import { outgoingMessage, incomingMessage } from '../utils/testData'
import {
    getBackendBaseUri,
    MESSAGES_IN_ENDPOINT,
    MESSAGES_OUT_ENDPOINT,
} from '../constants'

const headers = {
    headers: { 'Content-type': 'application/json' },
}

/**
 * Fetching related functions for messages for an organization.
 * @param {string} orgId
 *  The logged in user's organization id
 * @returns in and outgoing messages, loading indicator, functions to search for filtered messages and an error object.
 */
export default (orgId: string) => {
    const [tempInMessages, setTempInMessages] = useState<Message[]>([])
    const [tempOutMessages, setTempOutMessages] = useState<Message[]>([])
    const [searchedMessage, setSearchedMessage] = useState<Message | null>(null)
    const [isFetchingIn, setIsFetchingIn] = useState<boolean>(true)
    const [isFetchingOut, setIsFetchingOut] = useState<boolean>(true)
    const [error, setError] = useState<string>('')

    /**
     * Fetches ingoing messages for the selected organization based on a specified sender, and updates the 'tempInMessages' state.
     * @param {string} id
     *  The sender's organization id.
     */
    const fetchBySenderId = async (id: string): Promise<void> => {
        setIsFetchingIn(true)
        try {
            const endpoint = `${getBackendBaseUri()}/${orgId}/messages/sender/${id}`
            const response = await axios.get<Message[]>(endpoint, headers)
            httpResponseHandler(response, setTempInMessages, setError)
        } catch (exception) {
            console.log(`Error when fetching messages: ${exception}`)
            setError(`Error when fetching messages: ${exception}`)
        }
        setIsFetchingIn(false)
    }

    /**
     * Fetches outgoing messages for the selected organization based on a specified recipient.
     * @param {string} id
     *  The recipient's organization id.
     */
    const fetchByReceiverId = async (id: string): Promise<void> => {
        setIsFetchingOut(true)
        try {
            const endpoint = `${getBackendBaseUri()}/${orgId}/messages/receiver/${id}`
            const response = await axios.get<Message[]>(endpoint, headers)
            httpResponseHandler(response, setTempOutMessages, setError)
        } catch (exception) {
            console.log(`Error when fetching messages: ${exception}`)
            setError(`Error when fetching messages: ${exception}`)
        }
        setIsFetchingOut(false)
    }

    /**
     * Fetches outgoing messages for the selected organization based on a specified recipient.
     * @param {string} messageId
     *  The recipient's organization id.
     */
    const fetchByMessageId = async (messageId: string): Promise<void> => {
        try {
            const endpoint = `${getBackendBaseUri()}/${orgId}/messages/${messageId}`
            const response = await axios.get<Message>(endpoint, headers)
            httpObjectResponseHandler(response, setSearchedMessage, setError)
        } catch (exception) {
            console.log(`Error when fetching messages: ${exception}`)
            setError(`Error when fetching messages: ${exception}`)
        }
    }

    /**
     * Fetches either incoming or outgoing messages for an organization, and sets the state with the messages.
     *
     * @param {string } endpoint
     *  The endpoint messages will be fetched from.
     */
    const fetchMessages = async (endpoint: string): Promise<void> => {
        if (endpoint === MESSAGES_IN_ENDPOINT) {
            setIsFetchingIn(true)
        } else {
            setIsFetchingOut(true)
        }
        // const messages: Message[] = []

        // for (let i = 0; i < 200; i++) {
        //     messages.push(
        //         endpoint === MESSAGES_IN_ENDPOINT ? incomingMessage : outgoingMessage
        //     )
        // }
        // endpoint === MESSAGES_IN_ENDPOINT
        //     ? setTempInMessages(messages)
        //     : setTempOutMessages(messages)
        const tempUrl =
            endpoint === MESSAGES_IN_ENDPOINT
                ? `${getBackendBaseUri()}/${orgId}${MESSAGES_IN_ENDPOINT}`
                : `${getBackendBaseUri()}/${orgId}${MESSAGES_OUT_ENDPOINT}`
        try {
            const response = await axios.get<Message[]>(tempUrl, headers)

            httpResponseHandler(
                response,
                endpoint === MESSAGES_IN_ENDPOINT
                    ? setTempInMessages
                    : setTempOutMessages,
                setError
            )
        } catch (exception) {
            console.log(`Error when fetching messages: ${exception}`)
            setError(`Error when fetching messages: ${exception}`)
        }
        if (endpoint === MESSAGES_IN_ENDPOINT) {
            setIsFetchingIn(false)
        } else {
            setIsFetchingOut(false)
        }
    }

    useEffect(() => {
        fetchMessages(MESSAGES_IN_ENDPOINT)
        fetchMessages(MESSAGES_OUT_ENDPOINT)

        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    return {
        tempInMessages,
        tempOutMessages,
        searchedMessage,
        isFetchingIn,
        isFetchingOut,
        error,
        setError,
        setSearchedMessage,
        fetchBySenderId,
        fetchByReceiverId,
        fetchByMessageId,
        fetchMessages,
    }
}
