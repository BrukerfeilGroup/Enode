import React from 'react'
import Message from '../../../types/Message'
import SearchSenderReceiver from '../../containers/SearchSenderReceiver'
import MessageTable from './MessageTable'
import styles from './styles.module.css'

type InOrOutboxProps = {
    direction: 'IN' | 'OUT'
    messages: Message[]
    filteredOrgId?: string
    onChangeActive: (messageId: string) => void
    onSearch: (id: string) => void
    onClearFilteredMessages: () => void
}

const InOrOutbox: React.FC<InOrOutboxProps> = props => {
    const dirCheck = props.direction === 'IN'

    return (
        <div className={styles.table}>
            <div className={styles.titleContainer}>
                <h2 className={styles.title}>{dirCheck ? 'Innkommende' : 'Utgående'}</h2>
                <SearchSenderReceiver
                    placeholder={dirCheck ? 'Søk på avsender' : 'Søk på mottaker'}
                    onSearch={id => props.onSearch(id)}
                    onClear={props.onClearFilteredMessages}
                    filteredOrgId={props.filteredOrgId}
                />
            </div>

            <MessageTable
                dirCheck={dirCheck}
                messages={props.messages}
                onChangeActive={props.onChangeActive}
            />
        </div>
    )
}

export default InOrOutbox
