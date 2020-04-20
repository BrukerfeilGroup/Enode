import React from 'react'
import styles from './styles.module.css'

type SearchMoreRowProps = {
    orgIdToBeSearched: string
    onClick: (orgId: string) => void
    rowSpan: number
}

const SearchMoreRow: React.FC<SearchMoreRowProps> = props => {
    return (
        <tr
            className={styles.searchMoreRow}
            onClick={() => props.onClick(props.orgIdToBeSearched)}
        >
            <td colSpan={props.rowSpan}>
                Søk på '{props.orgIdToBeSearched}' for flere resultater
            </td>
        </tr>
    )
}

export default SearchMoreRow
