import React, { useEffect } from 'react'
import { useState } from 'react'
import { FaSearch, FaTimes } from 'react-icons/fa'
import styles from './styles.module.css'

type SearchSenderReceiverProps = {
    placeholder: string
    onSearch: (id: string) => void
    onClear: () => void
    onChange?: (input: string) => void
    filteredOrgId?: string //The organization id which a user has filtered on. Used to validate if a user should be allowed to clear the search
    styling?: StylesType //Used to override the styles. The overridden styling object must have the same classes which this components' css has
}

type StylesType = {
    readonly [key: string]: string
}

const SearchSenderReceiver: React.FC<SearchSenderReceiverProps> = ({
    styling = styles,
    ...props
}) => {
    const [senderRecipientId, setSenderRecipientId] = useState<any>('') //Use the any type

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        if (senderRecipientId) {
            e.preventDefault()
            //Special case where the user is allowed to enter whitespace
            /*if (isNaN(senderRecipientId)) {
                console.log('This is not a number')
                setSenderRecipientId('')
                return //Handle not a number. No response is given to the user because this action is blocked by input's type="number".
            }*/
            props.onSearch(senderRecipientId)
        } else {
            e.preventDefault()
        }
    }

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (props.onChange) props.onChange(e.target.value)
        setSenderRecipientId(e.target.value)
    }

    const handleClear = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.preventDefault()
        //Ensures the user has already searched before clearing the search. This will avoid spamming the clear button
        if (props.filteredOrgId) {
            props.onClear()
            setSenderRecipientId('')
        }
    }

    useEffect(() => {
        if (props.filteredOrgId) setSenderRecipientId(props.filteredOrgId)
    }, [props])

    return (
        <div className={styles.searchContainer}>
            <form onSubmit={handleSubmit}>
                <fieldset className={styling.fieldset}>
                    <input
                        className={styling.search}
                        //type="number"
                        //maxLength={11} //Max 11 so personalnr(11 digits) and orgnr(9digits) can be inserted
                        value={senderRecipientId}
                        onChange={handleChange}
                        placeholder={props.placeholder}
                    />
                    <button className={styling.confirmButton}>
                        <FaSearch />
                    </button>
                    <button className={styling.clearButton} onClick={handleClear}>
                        <FaTimes />
                    </button>
                </fieldset>
            </form>
        </div>
    )
}

export default SearchSenderReceiver
