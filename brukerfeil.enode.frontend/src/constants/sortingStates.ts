import { ElementsSortingState, DifiSortingState } from '../types/SortingTypes'

export const initialDifiSortingState: DifiSortingState = {
    conversationId: 'unsorted',
    created: 'unsorted',
    lastUpdate: 'unsorted',
    latestMessageStatus: 'unsorted',
    serviceIdentifier: 'unsorted',
    senderIdentifier: 'unsorted',
    receiverIdentifier: 'unsorted',
}

export const initialElementsSortingState: ElementsSortingState = {
    conversationId: 'unsorted',
    externalId: 'unsorted',
    lastUpdated: 'unsorted',
    createdDate: 'unsorted',
    sendingStatus: 'unsorted',
}
