export type ElementsStatus =
    | 'Klar for sending'
    | 'Mottatt elektronisk'
    | 'Sendt'
    | 'Under sending'
    | 'Signert'
    | 'Levert'
    | 'Åpnet  av mottaker'
    | 'Overføring feilet'

export type ElementsMessage = {
    id: number
    createdDate: Date
    isRead: boolean
    conversationId: string
    sendingMethod: string
    sendingStatus: {
        description: ElementsStatus
    } | null
}
