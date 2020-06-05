import React from 'react'
import { useState } from 'react'
import { FaSearch } from 'react-icons/fa'
import styles from './styles.module.css'

type SearchbarProps = {
    placeholder: string
    onSearch: (id: string) => void
    disabled: boolean
    onChange?: (input: string) => void
    styling?: StylesType //Used to override the styles. The overridden styling object must have the same classes which this components' css has
}

type StylesType = {
    readonly [key: string]: string
}

const Searchbar: React.FC<SearchbarProps> = ({ styling = styles, ...props }) => {
    const [searchPropertyId, setSearchPropertyId] = useState<any>('') //Use the any type

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        if (searchPropertyId) {
            e.preventDefault()
            props.onSearch(searchPropertyId)
        }
    }

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (props.onChange) props.onChange(e.target.value)
        setSearchPropertyId(e.target.value)
    }

    return (
        <form onSubmit={handleSubmit} className={styling.form}>
            <input
                className={styling.search}
                type="text"
                value={searchPropertyId}
                onChange={handleChange}
                placeholder={props.placeholder}
                disabled={props.disabled}
            />
            <button className={styling.confirmButton} disabled={!searchPropertyId}>
                <FaSearch />
            </button>
        </form>
    )
}

export default Searchbar
