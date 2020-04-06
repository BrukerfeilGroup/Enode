import React, { useState, useEffect } from 'react'
import styles from './styles.module.css'

type FilterElement = {
    name: string
    toggled: boolean
}

export type CheckboxStructure = {
    latestMessageStatus: FilterElement[]
    serviceIdentifier: FilterElement[]
}

export type Filters = {
    [key: string]: string[]
}

type Categories = 'latestMessageStatus' | 'serviceIdentifier'

type FilterBoxProps = {
    onFilterChange: (filters: Filters) => void
    onCheckboxChange: (checkbox: CheckboxStructure) => void
    initialFilter: Filters
    checkboxStructure: CheckboxStructure
}
const FilterBox: React.FC<FilterBoxProps> = props => {
    const [checkboxes, setCheckboxes] = useState<CheckboxStructure>(
        props.checkboxStructure
    )
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
                        key={i}
                    ></input>
                    <label htmlFor={f.name}>{f.name}</label>
                </li>
            </React.Fragment>
        ))
    }

    useEffect(() => {
        props.onFilterChange(filters)
        props.onCheckboxChange(checkboxes)
        // eslint-disable-next-line
    }, [filters, checkboxes])

    return (
        <div className={styles.container}>
            <div className={styles.content}>
                <p className={styles.header}>Status</p>
                <p className={styles.header2}>Type</p>
            </div>
            <div className={styles.content2}>
                <div className={styles.table}>
                    <ul>{renderFilters('latestMessageStatus')}</ul>
                </div>

                <div className={styles.table2}>
                    <ul>{renderFilters('serviceIdentifier')}</ul>
                    <div className={styles.bar}></div>
                </div>
            </div>
        </div>
    )
}
export default FilterBox
