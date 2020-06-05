export type DifiMessageKeys =
    | 'conversationId'
    | 'created'
    | 'lastUpdate'
    | 'serviceIdentifier'
    | 'senderIdentifier'
    | 'receiverIdentifier'
    | 'latestMessageStatus'

export type DifiSortingState = {
    [key in DifiMessageKeys]: Sorts
}

export type ElementsMessageKeys =
    | 'conversationId'
    | 'externalId'
    | 'lastUpdated'
    | 'createdDate'
    | 'sendingStatus'

export type ElementsSortingState = {
    [key in ElementsMessageKeys]: Sorts
}

export type Sorts = 'unsorted' | 'ascending' | 'descending'
