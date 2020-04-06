import { statusChecker, filterMessages } from './utils'
import Message from '../types/Message'
import { Filters } from '../components/containers/Filter'
const opprettYellowExpect = {
    style: 'yellowStatus',
}
const sendtYellowExpect = {
    style: 'yellowStatus',
}
const mottattYellowExpect = {
    style: 'yellowStatus',
}
const levertGreenExpect = {
    style: 'greenStatus',
}
const lestGreenExpect = {
    style: 'greenStatus',
}
const feilRedExpect = {
    style: 'redStatus',
}
const annetGreyExpect = {
    style: 'greyStatus',
}
const innkmYellowExpect = {
    style: 'yellowStatus',
}
const innklGreenExpect = {
    style: 'greenStatus',
}
const leveTidRedExpect = {
    style: 'redStatus',
}
describe('statusChecker', () => {
    it('should return yellowStatus style', () => {
        const result = statusChecker('OPPRETTET')
        expect(result).toEqual(opprettYellowExpect)
    })
    it('should return yellowStatus style', () => {
        const result = statusChecker('SENDT')
        expect(result).toEqual(sendtYellowExpect)
    })
    it('should return yellowStatus style', () => {
        const result = statusChecker('MOTTATT')
        expect(result).toEqual(mottattYellowExpect)
    })
    it('should return greenStatus style', () => {
        const result = statusChecker('LEVERT')
        expect(result).toEqual(levertGreenExpect)
    })
    it('should return greenStatus style', () => {
        const result = statusChecker('LEST')
        expect(result).toEqual(lestGreenExpect)
    })
    it('should return redStatus style', () => {
        const result = statusChecker('FEIL')
        expect(result).toEqual(feilRedExpect)
    })
    it('should return greyStatus style', () => {
        const result = statusChecker('ANNET')
        expect(result).toEqual(annetGreyExpect)
    })
    it('should return yellowStatus style', () => {
        const result = statusChecker('INNKOMMENDE_MOTTATT')
        expect(result).toEqual(innkmYellowExpect)
    })
    it('should return greenStatus style', () => {
        const result = statusChecker('INNKOMMENDE_LEVERT')
        expect(result).toEqual(innklGreenExpect)
    })
    it('should return redStatus style', () => {
        const result = statusChecker('LEVETID_UTLOPT')
        expect(result).toEqual(leveTidRedExpect)
    })
})
describe('statusCheckerNotNull', () => {
    it('should return yellowStatus style', () => {
        const result = statusChecker('OPPRETTET')
        expect(result).not.toBeNull()
    })
    it('should return yellowStatus style', () => {
        const result = statusChecker('SENDT')
        expect(result).not.toBeNull()
    })
    it('should return yellowStatus style', () => {
        const result = statusChecker('MOTTATT')
        expect(result).not.toBeNull()
    })
    it('should return greenStatus style', () => {
        const result = statusChecker('LEVERT')
        expect(result).not.toBeNull()
    })
    it('should return greenStatus style', () => {
        const result = statusChecker('LEST')
        expect(result).not.toBeNull()
    })
    it('should return redStatus style', () => {
        const result = statusChecker('FEIL')
        expect(result).not.toBeNull()
    })
    it('should return greyStatus style', () => {
        const result = statusChecker('ANNET')
        expect(result).not.toBeNull()
    })
    it('should return yellowStatus style', () => {
        const result = statusChecker('INNKOMMENDE_MOTTATT')
        expect(result).not.toBeNull()
    })
    it('should return greenStatus style', () => {
        const result = statusChecker('INNKOMMENDE_LEVERT')
        expect(result).not.toBeNull()
    })
    it('should return redStatus style', () => {
        const result = statusChecker('LEVETID_UTLOPT')
        expect(result).not.toBeNull()
    })
})

const messages: Message[] = [
    {
        id: 57355,
        conversationId: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
        messageId: '75c1002f-72ad-47dc-b738-884bac00ee78',
        senderIdentifier: 889640782,
        receiverIdentifier: 987464291,
        processIdentifier:
            'urn:no:difi:profile:arkivmelding:administrasjon:ver1.0',
        messageReference: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
        messageTitle: '',
        lastUpdate: new Date(Date.now()),
        finished: true,
        expiry: new Date(Date.now()),
        direction: 'INCOMING',
        serviceIdentifier: 'DPF',
        latestMessageStatus: 'LEVETID_UTLOPT',
        created: new Date(Date.now()),
    },
    {
        id: 57369,
        conversationId: 'bb3e40cd-250a-4deb-9b3a-a7712d18c899',
        messageId: '72d7122e-9c95-4c3e-9d6d-ce230d3abe07',
        senderIdentifier: 987464291,
        receiverIdentifier: 889640782,
        processIdentifier:
            'urn:no:difi:profile:arkivmelding:administrasjon:ver1.0',
        messageReference: 'bb3e40cd-250a-4deb-9b3a-a7712d18c899',
        messageTitle: 'Testtittel14',
        lastUpdate: new Date('2020-02-26T13:29:07.272+01:00'),
        finished: true,
        expiry: new Date('2020-05-10T02:31:52+02:00'),
        direction: 'OUTGOING',
        serviceIdentifier: 'DPO',
        latestMessageStatus: 'LEVERT',
        created: new Date('2020-02-26T13:28:22.039+01:00'),
    },
    {
        id: 57360,
        conversationId: 'dd9ba53b-2444-4de7-b21d-d32e25141b1e',
        messageId: '0646057f-8692-4c52-9181-a3a84d5049b0',
        senderIdentifier: 987464291,
        receiverIdentifier: 974761084,
        processIdentifier:
            'urn:no:difi:profile:arkivmelding:administrasjon:ver1.0',
        messageReference: 'dd9ba53b-2444-4de7-b21d-d32e25141b1e',
        messageTitle: 'Nye lysrør',
        lastUpdate: new Date('2020-02-25T16:17:52.372+01:00'),
        finished: true,
        expiry: new Date('2020-05-10T02:31:52+02:00'),
        direction: 'OUTGOING',
        serviceIdentifier: 'DPF',
        latestMessageStatus: 'MOTTATT',
        created: new Date('2020-02-25T16:13:25.55+01:00'),
    },
    {
        id: 57350,
        conversationId: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
        messageId: '75c1002f-72ad-47dc-b738-884bac00ee78',
        senderIdentifier: 889640782,
        receiverIdentifier: 987464291,
        processIdentifier:
            'urn:no:difi:profile:arkivmelding:administrasjon:ver1.0',
        messageReference: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
        messageTitle: '',
        lastUpdate: new Date(Date.now()),
        finished: true,
        expiry: new Date(Date.now()),
        direction: 'INCOMING',
        serviceIdentifier: 'DPO',
        latestMessageStatus: 'INNKOMMENDE_LEVERT',
        created: new Date(Date.now()),
    },
]

const expectedMessagesFirst: Message[] = [
    {
        id: 57355,
        conversationId: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
        messageId: '75c1002f-72ad-47dc-b738-884bac00ee78',
        senderIdentifier: 889640782,
        receiverIdentifier: 987464291,
        processIdentifier:
            'urn:no:difi:profile:arkivmelding:administrasjon:ver1.0',
        messageReference: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
        messageTitle: '',
        lastUpdate: new Date(Date.now()),
        finished: true,
        expiry: new Date(Date.now()),
        direction: 'INCOMING',
        serviceIdentifier: 'DPF',
        latestMessageStatus: 'LEVETID_UTLOPT',
        created: new Date(Date.now()),
    },
    {
        id: 57360,
        conversationId: 'dd9ba53b-2444-4de7-b21d-d32e25141b1e',
        messageId: '0646057f-8692-4c52-9181-a3a84d5049b0',
        senderIdentifier: 987464291,
        receiverIdentifier: 974761084,
        processIdentifier:
            'urn:no:difi:profile:arkivmelding:administrasjon:ver1.0',
        messageReference: 'dd9ba53b-2444-4de7-b21d-d32e25141b1e',
        messageTitle: 'Nye lysrør',
        lastUpdate: new Date('2020-02-25T16:17:52.372+01:00'),
        finished: true,
        expiry: new Date('2020-05-10T02:31:52+02:00'),
        direction: 'OUTGOING',
        serviceIdentifier: 'DPF',
        latestMessageStatus: 'MOTTATT',
        created: new Date('2020-02-25T16:13:25.55+01:00'),
    },
]

const filtersFirst: Filters = {
    latestMessageStatus: ['MOTTATT', 'LEVETID_UTLOPT'],
    serviceIdentifier: ['DPF'],
}

const expectedMessagesSecond: Message[] = [
    {
        id: 57350,
        conversationId: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
        messageId: '75c1002f-72ad-47dc-b738-884bac00ee78',
        senderIdentifier: 889640782,
        receiverIdentifier: 987464291,
        processIdentifier:
            'urn:no:difi:profile:arkivmelding:administrasjon:ver1.0',
        messageReference: '2d36a4ae-dd35-4831-9a44-b69fcd48c989',
        messageTitle: '',
        lastUpdate: new Date(Date.now()),
        finished: true,
        expiry: new Date(Date.now()),
        direction: 'INCOMING',
        serviceIdentifier: 'DPO',
        latestMessageStatus: 'INNKOMMENDE_LEVERT',
        created: new Date(Date.now()),
    },
]

const filtersSecond: Filters = {
    latestMessageStatus: ['INNKOMMENDE_LEVERT'],
    serviceIdentifier: [],
}

const filtersThird: Filters = {
    latestMessageStatus: ['FEIL'],
    serviceIdentifier: ['DPE'],
}

describe('filterMessages(Message[], Filters)', () => {
    it('should filter messagearray and return two messageobjects', () => {
        const result = filterMessages(messages, filtersFirst)
        expect(result).toEqual(expectedMessagesFirst)
    })
    it('should filter messagearray and return one messageobject', () => {
        const result = filterMessages(messages, filtersSecond)
        expect(result).toEqual(expectedMessagesSecond)
    })

    it('should not be null', () => {
        const result = filterMessages(messages, filtersFirst)
        expect(result).not.toBeNull()
    })

    it('aray should be empty', () => {
        const result = filterMessages(messages, filtersThird)
        expect(result).toEqual([])
    })
})
