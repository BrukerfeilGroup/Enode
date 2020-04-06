import { renderHook } from '@testing-library/react-hooks'
import { mocked } from 'ts-jest/utils'
import useOrganizations from './useOrganizations'
import axios from 'axios'
import Organization from '../types/Organization'

jest.mock('axios')

const axiosMocked = mocked(axios, true)

const expectedOrganizations: Organization[] = [
    {
        organizationId: 989778471,
        organizationName: 'Sandnes Kommune',
        database: 'au.com.google.Domainer',
        ExternalSystemName: 'gov',
    },
    {
        organizationId: 991825827,
        organizationName: 'Kristiansand Kommune',
        database: 'com.devhub.Tin',
        ExternalSystemName: 'gov',
    },
    {
        organizationId: 987464291,
        organizationName: 'Harstad Kommune',
        database: 'net.cpanel.Bitchip',
        ExternalSystemName: 'gov',
    },
    {
        organizationId: 989778471,
        organizationName: 'Stavanger Kommune',
        database: 'org.w3.Veribet',
        ExternalSystemName: 'net',
    },
    {
        organizationId: 991825827,
        organizationName: 'Atea',
        database: 'com.trellian.Alphazap',
        ExternalSystemName: 'org',
    },
    {
        organizationId: 987464291,
        organizationName: 'Laanekassen',
        database: 'com.trellian.Alphazap',
        ExternalSystemName: 'org',
    },
    {
        organizationId: 987464291,
        organizationName: 'Skatteetaten',
        database: 'com.trellian.Alphazap',
        ExternalSystemName: 'org',
    },
    {
        organizationId: 989778471,
        organizationName: 'Viken Fylkeskommune',
        database: 'au.com.google.Domainer',
        ExternalSystemName: 'gov',
    },
]

describe('useOrganizations()', () => {
    describe('Initial states', () => {
        axiosMocked.get.mockResolvedValue({
            data: expectedOrganizations,
            status: 200,
            statusText: 'Ok',
        })

        test("initial state 'organizations' to be an empty array", async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )

            expect(result.current.organizations).toEqual([])
            await waitForNextUpdate()
        })

        test("initial state 'isFetching' to be true", async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )

            expect(result.current.isFetching).toBe(true)
            await waitForNextUpdate()
        })

        test("initial state 'error' to be an empty string", async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )

            expect(result.current.error).toBe('')
            await waitForNextUpdate()
        })
    })
    //-------------------------------------------------------------------------------------------------
    describe('HTTP status 200', () => {
        beforeEach(() => {
            axiosMocked.get.mockReset()
            axiosMocked.get.mockResolvedValue({
                data: expectedOrganizations,
                status: 200,
                statusText: 'Ok',
            })
        })

        it('should be fetching', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )

            expect(result.current.isFetching).toBe(true)
            await waitForNextUpdate()
        })

        it('should update the organizations state with the expected organizations', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.organizations).toBe(expectedOrganizations)
        })

        it('should set the fetching state to false', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.isFetching).toBe(false)
        })

        it('should have en empty error state', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.error).toBe('')
        })

        it('should call axios.get() 1 times', async () => {
            const { waitForNextUpdate } = renderHook(() => useOrganizations())
            await waitForNextUpdate()
            expect(axios.get).toBeCalledTimes(1)
        })
    })
    //-------------------------------------------------------------------------------------------------
    describe('HTTP status 204', () => {
        beforeEach(() => {
            axiosMocked.get.mockReset()
            axiosMocked.get.mockResolvedValue({
                data: [],
                status: 204,
                statusText: 'No content',
            })
        })

        it('should update the organization state with an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.organizations).toEqual([])
        })

        it('should set the fetching state to false', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.isFetching).toBe(false)
        })

        it('should have en empty error state', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.error).toBe('')
        })

        it('should call axios.get() 1 time', async () => {
            const { waitForNextUpdate } = renderHook(() => useOrganizations())
            await waitForNextUpdate()
            expect(axios.get).toBeCalledTimes(1)
        })
    })
    //-------------------------------------------------------------------------------------------------
    describe('HTTP status 400', () => {
        const expectedError = 'There was an error with your request'
        beforeEach(() => {
            axiosMocked.get.mockReset()
            axiosMocked.get.mockResolvedValue({
                data: expectedError,
                status: 400,
                statusText: 'Bad request',
            })
        })

        it('should update the organization state with an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.organizations).toEqual([])
        })

        it('should set the fetching state to false', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.isFetching).toBe(false)
        })

        it('should have a meaningful error state', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.error).toBe(expectedError)
        })

        it('should call axios.get() 1 time', async () => {
            const { waitForNextUpdate } = renderHook(() => useOrganizations())
            await waitForNextUpdate()
            expect(axios.get).toBeCalledTimes(1)
        })
    })
    //-------------------------------------------------------------------------------------------------
    describe('HTTP status 500', () => {
        const expectedError =
            'There was an error the server. Please try again later'
        beforeEach(() => {
            axiosMocked.get.mockReset()
            axiosMocked.get.mockResolvedValue({
                data: expectedError,
                status: 500,
                statusText: 'Internal Server Error',
            })
        })

        it('should update the organization state with an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.organizations).toEqual([])
        })

        it('should set the fetching state to false', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.isFetching).toBe(false)
        })

        it('should have a meaningful error state', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.error).toBe(expectedError)
        })

        it('should call axios.get() 1 times', async () => {
            const { waitForNextUpdate } = renderHook(() => useOrganizations())
            await waitForNextUpdate()
            expect(axios.get).toBeCalledTimes(1)
        })
    })
    //-------------------------------------------------------------------------------------------------

    describe('Uncommon HTTP status code', () => {
        const expectedError = "\nStatus code: 418. \nStatus text: I'm a teapot."
        beforeEach(() => {
            axiosMocked.get.mockReset()
            axiosMocked.get.mockResolvedValue({
                data: expectedError,
                status: 418,
                statusText: "I'm a teapot",
            })
        })

        it('should update the organization state with an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.organizations).toEqual([])
        })

        it('should set the fetching state to false', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.isFetching).toBe(false)
        })

        it('should have a meaningful error state', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useOrganizations()
            )
            await waitForNextUpdate()
            expect(result.current.error).toBe(expectedError)
        })

        it('should call axios.get() 1 times', async () => {
            const { waitForNextUpdate } = renderHook(() => useOrganizations())
            await waitForNextUpdate()
            expect(axios.get).toBeCalledTimes(1)
        })
    })
})
