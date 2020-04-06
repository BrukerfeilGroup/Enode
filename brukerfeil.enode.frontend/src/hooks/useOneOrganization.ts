import { useState, useEffect } from 'react'
import axios from 'axios'
import Organization from '../types/Organization'
import { httpResponseHandler } from '../utils/utils'
//import {BACKEND_BASEURL, ORGANIZATION_ENDPOINT} from '../constants'
import { TEMP_SINGLE_ORGANIZATION_ENDPOINT } from '../constants'

const headers = { 'Content-type': 'application/json' }

export default () => {
    const [organization, setOrganization] = useState<Organization[]>([])
    const [isFetching, setIsFetching] = useState<boolean>(false)
    const [error, setError] = useState<string>('')

    const fetchSingleOrganization = async () => {
        setIsFetching(true)

        try {
            const response = await axios.get<Organization[]>(
                `${TEMP_SINGLE_ORGANIZATION_ENDPOINT}`,
                { headers }
            )

            httpResponseHandler(response, setOrganization, setError)
        } catch (exception) {
            console.log(`Error when fetching Organization: ${exception}`)
            setError(`Error when fetching Organization: ${exception}`)
        }
        setIsFetching(false)
    }

    useEffect(() => {
        fetchSingleOrganization()
    }, [])

    return {
        organization,
        isFetching,
        error,
    }
}
