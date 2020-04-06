import React from 'react'
import styles from './styles.module.css'

type SearcField = {
    name: string
}

const SearchField: React.FC<SearcField> = props => {
    return (
        <form>
            <label>
                {props.name}
                <input className={styles.search} />
            </label>
        </form>
    )
}

export default SearchField
