import React, { useState } from 'react'
import styles from './styles.module.css'
import SearchSenderReceiver from '../SearchSenderReceiver'
import { FaChevronDown } from 'react-icons/fa'

type FilterBox = {
    onSearchSenderId: (id: string) => void
    onSearchReceiverId: (id: string) => void
    onClearIncomingMessages: () => void
    onClearOutgoingMessages: () => void
}

export const FilterBox: React.FC<FilterBox> = props => {
    const [expanded, setExpanded] = useState(false)

    const handleExpandContainer = (
        e: React.MouseEvent<HTMLDivElement, MouseEvent>
    ) => {
        e.preventDefault()
        setExpanded(!expanded)
    }
    return (
        <>
            <div className={styles.container} onClick={handleExpandContainer}>
                <FaChevronDown className={styles.icon} />
            </div>
            <div
                className={
                    !expanded
                        ? styles.compressedContainer
                        : styles.expandedContainer
                }
            >
                <div className={styles.content}>
                    <div className={styles.searchContent}>
                        <SearchSenderReceiver
                            placeholder={'Søk på SenderID'}
                            onSearch={id => props.onSearchSenderId(id)}
                            onClear={props.onClearIncomingMessages}
                        />
                        <SearchSenderReceiver
                            placeholder={'Søk på ReceiverID'}
                            onSearch={id => props.onSearchReceiverId(id)}
                            onClear={props.onClearOutgoingMessages}
                        />
                    </div>
                </div>
            </div>
        </>
    )
}
