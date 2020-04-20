export type ElementsStatus =
    | 'Klar for sending'
    | 'Mottatt elektronisk'
    | 'Sendt'
    | 'Under sending'
    | 'Signert'
    | 'Levert'
    | 'Åpnet  av mottaker'
    | 'Overføring feilet'

type SendingStatus = {
    description: ElementsStatus
}

type SendingMethod = {
    description: string
}

export type ElementsMessage = {
    id: number
    name: string | null
    registryEntryId: number
    createdDate: Date
    lastUpdated: Date
    isRead: boolean
    conversationId: string
    externalId: string
    sendingMethod: SendingMethod | null
    sendingStatus: SendingStatus | null
}
