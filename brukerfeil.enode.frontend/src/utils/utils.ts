import React from 'react'
import Message from '../types/Message'
import { Status } from '../types/DifiMessage'
import { AxiosResponse } from 'axios'
import { Filters, CheckboxStructure } from '../components/containers/Filter'
import { ElementsStatus } from '../types/ElementsMessage'
import {
    DifiSortingState,
    DifiMessageKeys,
    ElementsSortingState,
    ElementsMessageKeys,
} from '../types/SortingTypes'

export const elementsStatusChecker = (elementsStatus: ElementsStatus | undefined) => {
    let elementsStyle = ''
    switch (elementsStatus) {
        case undefined:
            elementsStyle = 'redStatus'
            break
        case 'Åpnet  av mottaker':
            elementsStyle = 'greenStatus'
            break
        case 'Levert':
            elementsStyle = 'greenStatus'
            break
        case 'Sendt':
            elementsStyle = 'yellowStatus'
            break
        case 'Signert':
            elementsStyle = 'yellowStatus'
            break
        case 'Klar for sending':
            elementsStyle = 'yellowStatus'
            break
        case 'Mottatt elektronisk':
            elementsStyle = 'yellowStatus'
            break
        case 'Under sending':
            elementsStyle = 'yellowStatus'
            break
        case 'Overføring feilet':
            elementsStyle = 'redStatus'
            break
        default:
    }
    return elementsStyle
}

//Switch case for determening the STATUS of a message
export const difiStatusChecker = (status: Status | undefined) => {
    let style = ''
    switch (status) {
        case undefined:
            style = 'greyStatus'
            break
        case 'OPPRETTET':
            style = 'yellowStatus'
            break
        case 'SENDT':
            style = 'yellowStatus'
            break
        case 'MOTTATT':
            style = 'yellowStatus'
            break
        case 'LEVERT':
            style = 'greenStatus'
            break
        case 'LEST':
            style = 'greenStatus'
            break
        case 'FEIL':
            style = 'redStatus'
            break
        case 'ANNET':
            style = 'greyStatus'
            break
        case 'INNKOMMENDE_MOTTATT':
            style = 'yellowStatus'
            break
        case 'INNKOMMENDE_LEVERT':
            style = 'greenStatus'
            break
        case 'LEVETID_UTLOPT':
            style = 'redStatus'
            break
        default:
    }
    return style
}

//METHOD FOR SORTING TABLE ROW
// export const sortTing = (messages: Array<Message>) => {
//     return messages.sort((m1, m2) => (m1.lastUpdate > m2.lastUpdate ? 1 : -1))
// }

/**
 * Parses a Date object to a human readable format.
 * @param {Date} dateObject
 *
 */
export const parseDate = (dateObject: Date): string => {
    return new Intl.DateTimeFormat('en-GB', {
        timeZone: 'Europe/Oslo',
        hour12: false,
        formatMatcher: 'best fit',
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
        hour: 'numeric',
        minute: 'numeric',
    }).format(dateObject)
}
//METHOD FOR SORTING TABLE ROW
// export const sortTing = (messages: Array<Message>) => {
//     return messages.sort((m1, m2) => (m1.lastUpdate > m2.lastUpdate ? 1 : -1))
// }

/**
 * Checks HTTP status codes and updates states accordingly
 *
 * @param response
 * An AxiosResponse object
 * @param stateUpdater
 * The setState-function to update the relevant state
 * @param errorUpdater
 * The setState-function to update the error state
 * @throws
 * Exception message includes https status code and text
 */
export const httpResponseHandler = <T>(
    response: AxiosResponse<T[]>,
    stateUpdater: React.Dispatch<React.SetStateAction<T[]>>,
    errorUpdater: React.Dispatch<React.SetStateAction<string>>
) => {
    if (response.status === 200) {
        stateUpdater(response.data)
    } else if (response.status === 204) {
        stateUpdater([])
    } else if (response.status === 400 || 500) {
        errorUpdater(response.data as any)
    } else {
        throw new Error(
            `\nStatus code: ${response.status}. \nStatus text: ${response.statusText}.`
        )
    }
}

/**
 * Checks HTTP status codes and updates states accordingly
 *
 * @param response
 * An AxiosResponse object
 * @param stateUpdater
 * The setState-function to update the relevant state
 * @param errorUpdater
 * The setState-function to update the error state
 * @throws
 * Exception message includes https status code and text
 */
export const httpObjectResponseHandler = <T>(
    response: AxiosResponse<T>,
    stateUpdater: React.Dispatch<React.SetStateAction<T | null>>,
    errorUpdater: React.Dispatch<React.SetStateAction<string>>
) => {
    if (response.status === 200) {
        stateUpdater(response.data)
    } else if (response.status === 204) {
        stateUpdater(Object)
    } else if (response.status === 400 || 500) {
        errorUpdater(response.data as any)
    } else {
        throw new Error(
            `\nStatus code: ${response.status}. \nStatus text: ${response.statusText}.`
        )
    }
}
/**
 * Returns a filtered message array based on given filters.
 *
 * @param messages
 * An array of messages
 * @param filters
 * A Filters object which has the selected filters
 */
export const filterMessages = (messages: Message[], filters: Filters): Message[] => {
    return messages.filter(m => {
        let expectedSum = 0
        let actualSum = 0
        Object.keys(filters).forEach(cat => {
            expectedSum =
                filters[cat] && filters[cat].length > 0 ? expectedSum + 1 : expectedSum
            filters[cat].forEach(value => {
                let propertyToBeFilteredOn = null
                if (!m.elementsMessage) {
                    propertyToBeFilteredOn =
                        cat === 'latestMessageStatus'
                            ? m.difiMessage.latestMessageStatus
                            : null
                }
                if (!m.difiMessage) {
                    propertyToBeFilteredOn =
                        cat === 'sendingStatus'
                            ? m.elementsMessage.sendingStatus?.description
                            : null
                }
                if (m.difiMessage && m.elementsMessage) {
                    propertyToBeFilteredOn =
                        cat === 'latestMessageStatus'
                            ? m.difiMessage.latestMessageStatus
                            : cat === 'sendingStatus'
                            ? m.elementsMessage.sendingStatus?.description
                            : null
                }
                if (propertyToBeFilteredOn === value) {
                    actualSum += 1
                }
            })
        })
        return expectedSum === actualSum
    })
}
/**
 * Generate a random number in a range
 *
 * @param min random number from
 * @param max random number to
 */
export const randomNumber = (min: number, max: number) => {
    return Math.floor(Math.random() * (max - min + 1) + min)
}

export const checkboxStructure: CheckboxStructure = {
    latestMessageStatus: [
        { name: 'ANNET', toggled: false },
        { name: 'OPPRETTET', toggled: false },
        { name: 'SENDT', toggled: false },
        { name: 'INNKOMMENDE_MOTTATT', toggled: false },
        { name: 'INNKOMMENDE_LEVERT', toggled: false },
        { name: 'MOTTATT', toggled: false },
        { name: 'LEVERT', toggled: false },
        { name: 'LEST', toggled: false },
        { name: 'FEIL', toggled: false },
        { name: 'LEVETID_UTLOPT', toggled: false },
    ],
    sendingStatus: [
        { name: 'Åpnet  av mottaker', toggled: false },
        { name: 'Levert', toggled: false },
        { name: 'Sendt', toggled: false },
        { name: 'Signert', toggled: false },
        { name: 'Klar for sending', toggled: false },
        { name: 'Mottatt elektronisk', toggled: false },
        { name: 'Under sending', toggled: false },
        { name: 'Overføring feilet', toggled: false },
    ],
}

export const initialFilter: Filters = {
    latestMessageStatus: [],
    sendingStatus: [],
}

export const getUpdatedDifiSortingState = (
    initialState: DifiSortingState,
    currentState: DifiSortingState,
    column: DifiMessageKeys
): DifiSortingState => {
    return {
        ...initialState,
        [column]:
            currentState[column] === 'unsorted' || currentState[column] === 'descending'
                ? 'ascending'
                : 'descending',
    }
}

export const getUpdatedElementsSortingState = (
    initialState: ElementsSortingState,
    currentState: ElementsSortingState,
    column: ElementsMessageKeys
): ElementsSortingState => {
    return {
        ...initialState,
        [column]:
            currentState[column] === 'unsorted' || currentState[column] === 'descending'
                ? 'ascending'
                : 'descending',
    }
}

export const capitalisation = (name: string) => {
    let capLetter = name.charAt(0).toUpperCase()
    let lowLetters = name
        .slice(1)
        .toLowerCase()
        .replace('_', ' ')
    return capLetter + lowLetters
}
