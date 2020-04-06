import React from 'react'
import Message from '../types/Message'
import { Status } from '../types/DifiMessage'
import { AxiosResponse } from 'axios'
import { Filters, CheckboxStructure } from '../components/containers/Filter'

//Switch case for determening the STATUS of a message
export const statusChecker = (status: Status) => {
    let style = ''
    switch (status) {
        case null:
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
    return { style }
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
                const propertyToBeFilteredOn =
                    cat === 'latestMessageStatus'
                        ? m.difiMessage.latestMessageStatus
                        : m.difiMessage.serviceIdentifier

                if (propertyToBeFilteredOn === value) {
                    actualSum += 1
                }
            })
        })
        return expectedSum === actualSum
    })
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
    serviceIdentifier: [
        { name: 'DPO', toggled: false },
        { name: 'DPV', toggled: false },
        { name: 'DPI', toggled: false },
        { name: 'DPF', toggled: false },
        { name: 'DPE', toggled: false },
    ],
}

export const initialFilter: Filters = {
    latestMessageStatus: [],
    serviceIdentifier: [],
}
