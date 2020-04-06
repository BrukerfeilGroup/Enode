type Direction = 'INCOMING' | 'OUTGOING'

type ServiceIdentifier = 'DPO' | 'DPV' | 'DPI' | 'DPF' | 'DPE'

export type MessageStatuses = {
    id: string
    lastUpdate: Date
    status: Status
}

export type Status =
    | 'OPPRETTET'
    | 'SENDT'
    | 'MOTTATT'
    | 'LEVERT'
    | 'LEST'
    | 'FEIL'
    | 'ANNET'
    | 'INNKOMMENDE_MOTTATT'
    | 'INNKOMMENDE_LEVERT'
    | 'LEVETID_UTLOPT'

export type DifiMessage = {
    id: number
    conversationId: string
    messageId: string
    senderIdentifier: number
    receiverIdentifier: number
    processIdentifier: string //urn:no:difi:profile:arkivmelding:administrasjon:ver1.0,
    messageReference: string //40c8ccb8-aed8-4e1f-b87e-e27c0895813d,
    messageTitle: string
    lastUpdate: Date //2020-02-03T22:22:25.061+01:00,
    finished: boolean //true,
    expiry: Date //2020-02-03T22:21:56.71+01:00,
    direction: Direction
    serviceIdentifier: ServiceIdentifier
    latestMessageStatus: Status
    created: Date
    messageStatuses: MessageStatuses[]
}
