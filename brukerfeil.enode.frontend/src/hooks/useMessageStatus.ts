import { useState, useEffect } from 'react'
import axios from 'axios'
import MessageStatus from '../types/MessageStatus'
import { BACKEND_BASEURL, MESSAGE_STATUS_ENDPOINT } from '../constants'

export default (messageId: string) => {
    const [statuses, setStatuses] = useState<MessageStatus[]>([])
    const [isFetching, setIsFetching] = useState<boolean>(true)
    const [error, setError] = useState<string>('')

    const fetchMessageStatus = async () => {
        try {
            const response = await axios.get<MessageStatus[]>(
                `${BACKEND_BASEURL}${MESSAGE_STATUS_ENDPOINT}/${messageId}`,
                {
                    method: 'GET',
                }
            )
            if (response.status === 200) {
                const result: MessageStatus[] = response.data
                setStatuses(result)
            }
        } catch (exception) {
            console.log(`Error when fetching Message Status: ${exception}`)
            setError(`Error when fetching Message Status: ${exception}`)
        }
        setIsFetching(false)
    }

    useEffect(() => {
        if (messageId) {
            fetchMessageStatus()
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [messageId])

    return { statuses, error, isFetching }
}
