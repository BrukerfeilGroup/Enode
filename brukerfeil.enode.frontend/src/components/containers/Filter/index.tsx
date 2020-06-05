import React, { useState, useEffect, useCallback } from 'react'
import styles from './styles.module.css'
import { capitalisation } from '../../../utils/utils'

type FilterElement = {
    name: string
    toggled: boolean
}

export type CheckboxStructure = {
    latestMessageStatus: FilterElement[]
    sendingStatus: FilterElement[]
}

export type Filters = {
    [key: string]: string[]
}

type Categories = 'latestMessageStatus' | 'sendingStatus'

type FilterBoxProps = {
    onFilterChange: (filters: Filters) => void
    onCheckboxChange: (checkbox: CheckboxStructure) => void
    initialFilter: Filters
    checkboxStructure: CheckboxStructure
    onCloseFilter: () => void
}
const FilterBox: React.FC<FilterBoxProps> = props => {
    // prettier-ignore
    const [checkboxes, setCheckboxes] = useState<CheckboxStructure>(props.checkboxStructure)
    const [filters, setFilters] = useState<Filters>(props.initialFilter)

    const handleCheckboxChange = (name: string, key: Categories) => {
        const updatedCategory = checkboxes[key].map(f => {
            if (f.name === name) {
                return { ...f, toggled: !f.toggled }
            }
            return f
        })

        setCheckboxes({ ...checkboxes, [key]: updatedCategory })
        if (!filters[key].find(f => f === name)) {
            setFilters({ ...filters, [key]: [...filters[key], name] })
        } else {
            setFilters({
                ...filters,
                [key]: filters[key].filter(value => value !== name),
            })
        }
    }

    const renderFilters = (key: Categories) => {
        return checkboxes[key].map((f, i) => (
            <React.Fragment key={i}>
                <li onClick={() => handleCheckboxChange(f.name, key)}>
                    <input
                        className={styles.checkBox}
                        type="checkbox"
                        name={f.name}
                        checked={f.toggled}
                        onChange={() => handleCheckboxChange(f.name, key)}
                        key={i}
                    ></input>
                    <label htmlFor={f.name}>{capitalisation(f.name)}</label>
                </li>
            </React.Fragment>
        ))
    }

    const escClick = useCallback(
        (event: KeyboardEvent) => {
            if (event.keyCode === 27) {
                props.onCloseFilter()
            }
        },
        [props]
    )

    // Adds event listener for clicking escape
    useEffect(() => {
        document.addEventListener('keydown', escClick, false)

        return () => {
            document.removeEventListener('keydown', escClick)
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    useEffect(() => {
        props.onFilterChange(filters)
        props.onCheckboxChange(checkboxes)
        // eslint-disable-next-line
    }, [filters, checkboxes])

    return (
        <div onClick={() => props.onCloseFilter()} className={styles.background}>
            <div onClick={event => event.stopPropagation()} className={styles.container}>
                <div className={styles.content}>
                    <p className={styles.header}>Difi status</p>
                    <p className={styles.header2}>Elements status</p>
                </div>
                <div className={styles.content2}>
                    <div className={styles.table}>
                        <ul>{renderFilters('latestMessageStatus')}</ul>
                    </div>
                    <div className={styles.bar}></div>
                    <div className={styles.table2}>
                        <ul>{renderFilters('sendingStatus')}</ul>
                    </div>
                </div>
            </div>
        </div>
    )
}
export default FilterBox
