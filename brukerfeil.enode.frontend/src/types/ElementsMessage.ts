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

type RegistryEntry = {
    id: number
    caseId: number
    documentNumber: number
    createdDate: Date
    lastUpdated: Date
    title: String
    senderRecipient: String
    isSenderRecipientRestricted: boolean
    case: Case
}

type Case = {
    id: number
    countOfRegistryEntries: number
    publicTitle: String
    caseNumber: String
    isRestricted: boolean
    createdDate: Date
    lastUpdated: Date
    caseDate: Date
}

export type ElementsMessage = {
    id: number
    name: string | null
    isPerson: boolean
    createdDate: Date
    lastUpdated: Date
    isRead: boolean
    conversationId: string
    externalId: string
    registryEntryId: number
    sendingMethod: SendingMethod | null
    sendingStatus: SendingStatus | null
    registryEntry: RegistryEntry | null
}
