import React, { useMemo } from 'react'
import Skeleton from 'react-loading-skeleton'
import styles from './styles.module.css'
import { v1 as uuid } from 'uuid'
import { randomNumber } from '../../../utils/utils'

const TableSkeleton: React.FC = () => {
    //useMemo in order for the skeleton elements not to change size when the component rerenders (because of parent state changing)
    const skeletonRows = useMemo(
        () =>
            Array.from('x'.repeat(50)).map((n, i) => (
                <tr key={uuid()}>
                    <td colSpan={2}>
                        <Skeleton width={randomNumber(40, 150)} />
                    </td>
                    <td colSpan={2}>
                        <Skeleton width={randomNumber(80, 160)} />
                    </td>
                    <td colSpan={3}>
                        <Skeleton width={randomNumber(70, 120)} />
                    </td>
                </tr>
            )),
        []
    )

    return (
        <table className={styles.table}>
            <thead>
                <tr className={styles.tableHeader}>
                    <th colSpan={2}>
                        <Skeleton width={120} />
                    </th>
                    <th colSpan={2}>
                        <Skeleton width={80} />
                    </th>
                    <th colSpan={3}>
                        <Skeleton width={140} />
                    </th>
                </tr>
            </thead>
            <tbody className={styles.loadingTbody}>{skeletonRows}</tbody>
        </table>
    )
}

export default TableSkeleton
