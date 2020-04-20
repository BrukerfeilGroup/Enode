import Message from '../types/Message'
import uuid from 'uuid/v1'
export const incomingMessage: Message = {
    difiMessage: {
        conversationId: uuid(),
        created: new Date('2020-04-02T07:40:59.8430236Z'),
        direction: 'INCOMING',
        expiry: new Date('2020-04-02T07:40:59.8430236Z'),
        finished: true,
        id: Math.random(),
        lastUpdate: new Date('2020-04-02T07:40:59.8430236Z'),
        latestMessageStatus: 'INNKOMMENDE_LEVERT',
        messageId: uuid(),
        messageReference: uuid(),
        serviceIdentifier: 'DPO',
        messageTitle: 'En testmelding',
        processIdentifier: 'urn:no:difi:profile:arkivmelding:administrasjon:ver1.0',
        receiverIdentifier: 917082308,
        senderIdentifier: 987464291,
        messageStatuses: [
            {
                id: '1',
                lastUpdate: new Date('2020-04-02T07:40:59.8430236Z'),
                status: 'INNKOMMENDE_MOTTATT',
            },
            {
                id: '2',
                lastUpdate: new Date('2020-04-02T07:40:59.8430236Z'),
                status: 'INNKOMMENDE_MOTTATT',
            },
            {
                id: '3',
                lastUpdate: new Date('2020-04-02T07:40:59.8430236Z'),
                status: 'INNKOMMENDE_MOTTATT',
            },
        ],
    },
    elementsMessage: {
        conversationId: uuid(),
        createdDate: new Date('2020-04-02T07:40:59.8430236Z'),
        externalId: '987464291',
        id: 134321,
        isRead: true,
        lastUpdated: new Date('2020-04-02T07:40:59.8430236Z'),
        name: 'Jens augusust veldig langt navn',
        registryEntryId: 13421,
        sendingMethod: null,
        sendingStatus: null,
    },
}

export const outgoingMessage: Message = {
    difiMessage: {
        conversationId: uuid(),
        created: new Date('2020-04-02T07:40:59.8430236Z'),
        direction: 'OUTGOING',
        expiry: new Date('2020-04-02T07:40:59.8430236Z'),
        finished: true,
        id: 1234,
        lastUpdate: new Date('2020-04-02T07:40:59.8430236Z'),
        latestMessageStatus: 'INNKOMMENDE_LEVERT',
        messageId: uuid(),
        messageReference: uuid(),
        serviceIdentifier: 'DPO',
        messageTitle: 'En testmelding',
        processIdentifier: 'urn:no:difi:profile:arkivmelding:administrasjon:ver1.0',
        receiverIdentifier: 917082308,
        senderIdentifier: 987464291,
        messageStatuses: [
            {
                id: '1',
                lastUpdate: new Date('2020-04-02T07:40:59.8430236Z'),
                status: 'OPPRETTET',
            },
            {
                id: '2',
                lastUpdate: new Date('2020-04-02T07:40:59.8430236Z'),
                status: 'LEVERT',
            },
            {
                id: '3',
                lastUpdate: new Date('2020-04-02T07:40:59.8430236Z'),
                status: 'LEST',
            },
        ],
    },
    elementsMessage: {
        conversationId: uuid(),
        createdDate: new Date('2020-04-02T07:40:59.8430236Z'),
        externalId: '987464291',
        id: 134321,
        isRead: true,
        lastUpdated: new Date('2020-04-02T07:40:59.8430236Z'),
        name: 'Jens augusust veldig langt navn',
        registryEntryId: 13421,
        sendingMethod: {
            description: 'Sikker digitalt brev',
        },
        sendingStatus: { description: 'Åpnet  av mottaker' },
    },
}
