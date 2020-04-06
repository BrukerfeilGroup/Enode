import React from 'react'
import { FilterBox } from './FilterBox'

type FilterBoxContainer = {
    onSearchSenderId: (id: string) => void
    onSearchReceiverId: (id: string) => void
    onClearIncomingMessages: () => void
    onClearOutgoingMessages: () => void
}

export const FilterBoxContainer: React.FC<FilterBoxContainer> = props => {
    return (
        <div>
            <FilterBox
                onSearchSenderId={props.onSearchSenderId}
                onSearchReceiverId={props.onSearchReceiverId}
                onClearIncomingMessages={props.onClearIncomingMessages}
                onClearOutgoingMessages={props.onClearOutgoingMessages}
            />
        </div>
    )
}
