import React from 'react'
import { Status } from './types/Message'
import { FaMinusCircle } from 'react-icons/fa'
import { FaEnvelope } from 'react-icons/fa'
import { MdSend } from 'react-icons/md'
import { MdError } from 'react-icons/md'
import { FaEnvelopeOpenText } from 'react-icons/fa'
import { FaQuestionCircle } from 'react-icons/fa'
import { IoIosTime } from 'react-icons/io'
import { MdMarkunreadMailbox } from 'react-icons/md'

//Switch case for determening the STATUS of a message
export const statusChecker = (status: Status) => {
    let Icon: React.FC = FaMinusCircle
    let style = ''
    let color = ''
    switch (status) {
        case null:
            Icon = FaMinusCircle
            style = 'greyStatus'
            color = 'white'
            break
        case 'SENDT':
            Icon = MdSend
            style = 'blueStatus'
            color = '#BEF45A'
            break
        case 'MOTTATT':
            Icon = MdMarkunreadMailbox
            style = 'purpleStatus'
            color = '#FFCCFF'
            break
        case 'LEVERT':
            Icon = FaEnvelope
            style = 'greenStatus'
            color = 'white'
            break
        case 'LEST':
            Icon = FaEnvelopeOpenText
            style = 'greenStatus'
            color = 'white'
            break
        case 'FEIL':
            Icon = MdError
            style = 'redStatus'
            color = 'white'
            break
        case 'ANNET':
            Icon = FaQuestionCircle
            style = 'greyStatus'
            color = 'black'
            break
        case 'INNKOMMENDE_MOTTATT':
            Icon = MdMarkunreadMailbox
            style = 'purpleStatus'
            color = '#FFCCFF'
            break
        case 'LEVETID_UTLOPT':
            Icon = IoIosTime
            style = 'redStatus'
            color = 'white'
            break
        default:
    }
    return { style, Icon, color }
}

//METHOD FOR SORTING TABLE ROW
// export const sortTing = (messages: Array<Message>) => {
//     return messages.sort((m1, m2) => (m1.lastUpdate > m2.lastUpdate ? 1 : -1))
// }
