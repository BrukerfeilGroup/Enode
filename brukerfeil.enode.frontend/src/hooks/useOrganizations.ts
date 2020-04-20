import { useState, useEffect } from 'react'
import axios from 'axios'
import Organization from '../types/Organization'
import { httpResponseHandler } from '../utils/utils'
import { getBackendBaseUri, ORGANIZATION_ENDPOINT } from '../constants'

const headers = { 'Content-type': 'application/json' }

export default () => {
    const [organizations, setOrganizations] = useState<Organization[]>([])
    const [isFetching, setIsFetching] = useState<boolean>(false)
    const [error, setError] = useState<string>('')

    const fetchOrganizations = async () => {
        setIsFetching(true)

        try {
            const response = await axios.get<Organization[]>(
                `${getBackendBaseUri()}${ORGANIZATION_ENDPOINT}`,
                {
                    headers,
                }
            )

            httpResponseHandler(response, setOrganizations, setError)
        } catch (exception) {
            console.log(`Error when fetching Organizations: ${exception}`)
            setError(`Error when fetching Organizations: ${exception}`)
        }
        setIsFetching(false)
    }

    useEffect(() => {
        fetchOrganizations()
    }, [])

    return {
        organizations,
        isFetching,
        error,
    }
}
