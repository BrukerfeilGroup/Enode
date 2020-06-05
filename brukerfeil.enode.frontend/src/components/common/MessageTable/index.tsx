import React from 'react'
import { MessageTableContainerProps } from '../../containers/MessageTableContainer'
import Searchbar from '../../containers/Searchbar'
import IncomingMessagesTable from './IncomingMessagesTable'
import OutgoingMessagesTable from './OutgoingMessagesTable'
import SearchMoreRow from './SearchMoreRow'
import classnames from 'classnames'
import styles from './styles.module.css'
import {
    DifiSortingState,
    ElementsSortingState,
    DifiMessageKeys,
    ElementsMessageKeys,
} from '../../../types/SortingTypes'

interface MessageTableProps extends MessageTableContainerProps {
    filterMessages: (input: string) => void
    difiSortingStates: DifiSortingState
    elementsSortingStates: ElementsSortingState
    onSortDifiMessages: (column: DifiMessageKeys) => void
    onSortElementsMessages: (column: ElementsMessageKeys) => void
}

const MessageTable: React.FC<MessageTableProps> = props => {
    const { incoming } = props

    return (
        <div
            className={
                props.messages.length < 1
                    ? classnames(styles.noScroll, styles.tableContainer)
                    : styles.tableContainer
            }
        >
            <div className={styles.titleContainer}>
                <h2 className={styles.title}>{incoming ? 'Innkommende' : 'Utgående'}</h2>
                <Searchbar
                    placeholder={incoming ? 'Søk på avsender' : 'Søk på mottaker'}
                    onSearch={id => props.onSearch(id)}
                    onChange={props.filterMessages}
                    disabled={props.isFetching}
                />
            </div>

            {incoming ? (
                <IncomingMessagesTable
                    messages={props.messages}
                    difiSortingStates={props.difiSortingStates}
                    onChangeActive={props.onChangeActive}
                    onSortDifi={props.onSortDifiMessages}
                    isFetching={props.isFetching}
                >
                    {props.filteredOrgId && props.filteredOrgId.length === 9 ? (
                        <SearchMoreRow
                            rowSpan={6}
                            onClick={props.onSearch}
                            orgIdToBeSearched={props.filteredOrgId}
                        />
                    ) : null}
                </IncomingMessagesTable>
            ) : (
                <OutgoingMessagesTable
                    messages={props.messages}
                    elementsSortingStates={props.elementsSortingStates}
                    difiSortingStates={props.difiSortingStates}
                    onChangeActive={props.onChangeActive}
                    onSortDifi={props.onSortDifiMessages}
                    onSortElements={props.onSortElementsMessages}
                    isFetching={props.isFetching}
                >
                    {props.filteredOrgId && props.filteredOrgId.length === 9 ? (
                        <SearchMoreRow
                            rowSpan={7}
                            onClick={props.onSearch}
                            orgIdToBeSearched={props.filteredOrgId}
                        />
                    ) : null}
                </OutgoingMessagesTable>
            )}
        </div>
    )
}

export default MessageTable
