import { renderHook, act } from '@testing-library/react-hooks'
import { mocked } from 'ts-jest/utils'
import axios from 'axios'
import useMessages from './useMessages'
import Message from '../types/Message'
import { MESSAGES_IN_ENDPOINT, MESSAGES_OUT_ENDPOINT } from '../constants'

jest.mock('axios')

const axiosMocked = mocked(axios, true)

const messages: Message[] = [
    {
        difiMessage: {
            id: 57355,
            conversationId: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
            messageId: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
            senderIdentifier: 889640782,
            receiverIdentifier: 987464291,
            processIdentifier: 'urn:no:difi:profile:arkivmelding:administrasjon:ver1.0',
            messageReference: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
            messageTitle: '',
            lastUpdate: new Date(Date.now()),
            finished: true,
            expiry: new Date(Date.now()),
            direction: 'INCOMING',
            serviceIdentifier: 'DPO',
            latestMessageStatus: 'LEVETID_UTLOPT',
            messageStatuses: [
                {
                    id: '870',
                    lastUpdate: new Date('2020-04-14T15:39:24.682+02:00'),
                    status: 'OPPRETTET',
                },
                {
                    id: '871',
                    lastUpdate: new Date('2020-04-14T15:39:24.682+02:00'),
                    status: 'INNKOMMENDE_MOTTATT',
                },
                {
                    id: '873',
                    lastUpdate: new Date('2020-04-15T15:39:27.047+02:00'),
                    status: 'LEVETID_UTLOPT',
                },
            ],
            created: new Date(Date.now()),
        },
        elementsMessage: {
            conversationId: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
            createdDate: new Date('2020-04-02T07:40:59.8430236Z'),
            externalId: '987464291',
            id: 134321,
            isRead: true,
            isPerson: true,
            lastUpdated: new Date('2020-04-02T07:40:59.8430236Z'),
            name: 'Jens augusust veldig langt navn',
            registryEntryId: 13421,
            sendingMethod: null,
            sendingStatus: null,
            registryEntry: {
                id: 2952,
                caseId: 3124,
                documentNumber: 321,
                createdDate: new Date('2020-03-17T10:56:55+01:00'),
                lastUpdated: new Date('2020-04-23T08:59:59.375+02:00'),
                title: 'test',
                senderRecipient: '922308055',
                isSenderRecipientRestricted: true,
                case: {
                    id: 3124,
                    countOfRegistryEntries: 15,
                    publicTitle: 'New',
                    caseNumber: '2020/298',
                    isRestricted: false,
                    createdDate: new Date('2020-03-17T10:58:55+01:00'),
                    lastUpdated: new Date('2020-03-17T10:60:55+01:00'),
                    caseDate: new Date('2020-03-17T10:56:55+01:00'),
                },
            },
        },
    },
    {
        difiMessage: {
            id: 57350,
            conversationId: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
            messageId: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
            senderIdentifier: 889640782,
            receiverIdentifier: 987464291,
            processIdentifier: 'urn:no:difi:profile:arkivmelding:administrasjon:ver1.0',
            messageReference: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
            messageTitle: '',
            lastUpdate: new Date(Date.now()),
            finished: true,
            expiry: new Date(Date.now()),
            direction: 'INCOMING',
            serviceIdentifier: 'DPO',
            latestMessageStatus: 'LEVETID_UTLOPT',
            created: new Date(Date.now()),
            messageStatuses: [
                {
                    id: '870',
                    lastUpdate: new Date('2020-04-14T15:39:24.682+02:00'),
                    status: 'OPPRETTET',
                },
                {
                    id: '871',
                    lastUpdate: new Date('2020-04-14T15:39:24.682+02:00'),
                    status: 'INNKOMMENDE_MOTTATT',
                },
                {
                    id: '873',
                    lastUpdate: new Date('2020-04-15T15:39:27.047+02:00'),
                    status: 'LEVETID_UTLOPT',
                },
            ],
        },
        elementsMessage: {
            conversationId: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
            createdDate: new Date('2020-04-02T07:40:59.8430236Z'),
            externalId: '987464291',
            id: 134321,
            isRead: true,
            isPerson: false,
            lastUpdated: new Date('2020-04-02T07:40:59.8430236Z'),
            name: 'Jens augusust veldig langt navn',
            registryEntryId: 13421,
            sendingMethod: null,
            sendingStatus: null,
            registryEntry: {
                id: 2952,
                caseId: 3124,
                documentNumber: 321,
                createdDate: new Date('2020-03-17T10:56:55+01:00'),
                lastUpdated: new Date('2020-04-23T08:59:59.375+02:00'),
                title: 'test',

                senderRecipient: '922308055',
                isSenderRecipientRestricted: true,
                case: {
                    id: 3124,
                    countOfRegistryEntries: 15,
                    publicTitle: 'New',
                    caseNumber: '2020/298',
                    isRestricted: false,
                    createdDate: new Date('2020-03-17T10:58:55+01:00'),
                    lastUpdated: new Date('2020-03-17T10:60:55+01:00'),
                    caseDate: new Date('2020-03-17T10:56:55+01:00'),
                },
            },
        },
    },
]
describe('useMessages()', () => {
    describe('Initial state values', () => {
        axiosMocked.get.mockResolvedValue({
            data: messages,
            status: 200,
            statusText: 'Ok',
        })

        test("initial state 'tempInMessages' to be an empty array", async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))

            expect(result.current.tempInMessages).toEqual([])
            await waitForNextUpdate()
        })

        test("initial state 'tempOutMessages' to be an empty array", async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))

            expect(result.current.tempOutMessages).toEqual([])
            await waitForNextUpdate()
        })

        test("initial state 'isFetching' to be true", async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages(MESSAGES_IN_ENDPOINT)
            )

            expect(result.current.isFetchingIn).toBe(true)
            await waitForNextUpdate()
        })

        test("initial state 'error' to be an empty string", async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))

            expect(result.current.error).toBe('')
            await waitForNextUpdate()
        })

        it('should return a function', async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))
            await waitForNextUpdate()

            expect(result.current.fetchByReceiverId).toBeInstanceOf(Function)
        })

        it('should return a function', async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))
            await waitForNextUpdate()

            expect(result.current.fetchBySenderId).toBeInstanceOf(Function)
        })
    })
    //-------------------------------------------------------------------------------------------------
    describe('HTTP 200 cases', () => {
        beforeEach(() => {
            axiosMocked.get.mockReset()
            axiosMocked.get.mockResolvedValue({
                data: messages,
                status: 200,
                statusText: 'Ok',
            })
        })

        it("should update the tempInMessages state to 'messsages'", async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))
            await waitForNextUpdate()

            expect(result.current.tempInMessages).toBe<Message[]>(messages)
        })

        it("should update the tempOutMessages state to 'messsages'", async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))
            await waitForNextUpdate()

            expect(result.current.tempOutMessages).toBe<Message[]>(messages)
        })

        it("should update the tempInMessages state to 'messsages'", async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))
            await act(async () => {
                result.current.fetchByReceiverId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.tempInMessages).toBe(messages)
        })

        it("should update the tempOutMessages state to 'messsages'", async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))
            await act(async () => {
                result.current.fetchByReceiverId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.tempOutMessages).toBe(messages)
        })

        it('should return an empty error', async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))
            await waitForNextUpdate()

            expect(result.current.error).toBe<string>('')
        })

        it('should call axios.get() three times', async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))
            await act(async () => {
                result.current.fetchByReceiverId('9999')
                await waitForNextUpdate()
            })

            expect(axios.get).toBeCalledTimes(3)
        })

        it('should call axios.get() 2 times', async () => {
            const { waitForNextUpdate } = renderHook(() => useMessages('1337'))
            await waitForNextUpdate()

            expect(axios.get).toBeCalledTimes(2)
        })
    })

    //----------------------------------------------------------------------------
    describe('HTTP 204 cases', () => {
        beforeEach(() => {
            axiosMocked.get.mockReset()
            axiosMocked.get.mockResolvedValue({
                data: '',
                status: 204,
                statusText: 'No content',
            })
        })

        it('should update the tempInMessages state to an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))
            await waitForNextUpdate()

            expect(result.current.tempInMessages).toEqual([])
        })

        it('should update the tempOutMessages state to an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages(MESSAGES_OUT_ENDPOINT)
            )
            await waitForNextUpdate()

            expect(result.current.tempOutMessages).toEqual([])
        })

        it('should update the tempInMessages state to an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages(MESSAGES_OUT_ENDPOINT)
            )
            await act(async () => {
                result.current.fetchByReceiverId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.tempInMessages).toEqual([])
        })

        it('should update the tempOutMessages state to an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages(MESSAGES_OUT_ENDPOINT)
            )
            await act(async () => {
                result.current.fetchByReceiverId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.tempOutMessages).toEqual([])
        })

        test('fetching is false', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages(MESSAGES_OUT_ENDPOINT)
            )
            await waitForNextUpdate()
            expect(result.current.isFetchingIn).toBe(false)
        })

        test('fetching is false after calling fetchByReceiverId()', async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))
            await act(async () => {
                result.current.fetchByReceiverId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.isFetchingOut).toEqual(false)
        })

        test('fetching is false after calling fetchBySenderId()', async () => {
            const { result, waitForNextUpdate } = renderHook(() => useMessages('1337'))
            await act(async () => {
                result.current.fetchBySenderId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.isFetchingIn).toEqual(false)
        })
    })

    //-----------------------------------------------------------------------------
    describe('HTTP 400 cases', () => {
        const expectedError = 'Please provide a numeric organization id'
        beforeEach(() => {
            axiosMocked.get.mockReset()
            axiosMocked.get.mockResolvedValue({
                data: expectedError,
                status: 400,
                statusText: 'Bad request',
            })
        })

        it('should return a meaningful error message', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages('asdf1337')
            )
            await waitForNextUpdate()
            expect(result.current.error).toBe(expectedError)
        })

        it('should update the tempInMessages state to an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages('asdf1337')
            )
            await waitForNextUpdate()

            expect(result.current.tempInMessages).toEqual([])
        })

        it('should update the tempOutMessages state to an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages('asdf1337')
            )
            await waitForNextUpdate()

            expect(result.current.tempOutMessages).toEqual([])
        })

        it('should update the tempInMessages state to an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages('asdf1337')
            )
            await act(async () => {
                result.current.fetchByReceiverId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.tempInMessages).toEqual([])
        })

        it('should update the tempOutMessages state to an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages('asdf1337')
            )
            await act(async () => {
                result.current.fetchByReceiverId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.tempOutMessages).toEqual([])
        })

        test('fetching is false', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages(MESSAGES_OUT_ENDPOINT)
            )
            await waitForNextUpdate()
            expect(result.current.isFetchingOut).toBe(false)
        })

        test('fetching is false after calling fetchByReceiverId()', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages(MESSAGES_OUT_ENDPOINT)
            )
            await act(async () => {
                result.current.fetchByReceiverId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.isFetchingOut).toEqual(false)
        })

        test('fetching is false after calling fetchBySenderId()', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages(MESSAGES_IN_ENDPOINT)
            )
            await act(async () => {
                result.current.fetchBySenderId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.isFetchingIn).toEqual(false)
        })
    })
    //-------------------------------------------------------------------------------------------------
    describe('HTTP 500 cases', () => {
        const expectedError = 'There was an error the server. Please try again later'
        beforeEach(() => {
            axiosMocked.get.mockReset()
            axiosMocked.get.mockResolvedValue({
                data: expectedError,
                status: 500,
                statusText: 'Internal Server Error',
            })
        })

        it('should return a meaningful error message', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages('asdf1337')
            )
            await waitForNextUpdate()
            expect(result.current.error).toBe(expectedError)
        })

        it('should update the tempInMessages state to an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages('asdf1337')
            )
            await waitForNextUpdate()

            expect(result.current.tempInMessages).toEqual([])
        })

        it('should update the tempOutMessages state to an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages('asdf1337')
            )
            await waitForNextUpdate()

            expect(result.current.tempOutMessages).toEqual([])
        })

        it('should update the tempInMessages state to an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages('asdf1337')
            )
            await act(async () => {
                result.current.fetchByReceiverId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.tempInMessages).toEqual([])
        })

        it('should update the tempOutMessages state to an empty array', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages('asdf1337')
            )
            await act(async () => {
                result.current.fetchByReceiverId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.tempOutMessages).toEqual([])
        })

        test('fetching is false', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages(MESSAGES_OUT_ENDPOINT)
            )
            await waitForNextUpdate()
            expect(result.current.isFetchingOut).toBe(false)
        })

        test('fetching is false after calling fetchByReceiverId()', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages(MESSAGES_OUT_ENDPOINT)
            )
            await act(async () => {
                result.current.fetchByReceiverId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.isFetchingOut).toEqual(false)
        })

        test('fetching is false after calling fetchBySenderId()', async () => {
            const { result, waitForNextUpdate } = renderHook(() =>
                useMessages(MESSAGES_OUT_ENDPOINT)
            )
            await act(async () => {
                result.current.fetchBySenderId('9999')
                await waitForNextUpdate()
            })

            expect(result.current.isFetchingIn).toEqual(false)
        })
    })
})
