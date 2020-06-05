import React from 'react'
import useScrollable from '../../../hooks/useScrollable'
import { Link } from 'react-router-dom'
import styles from './styles.module.css'
import { useEffect, useCallback } from 'react'

type HamburgerMenuProps = {
    org: string
    onCloseHamburger: () => void
}

const HamburgerMenu: React.FC<HamburgerMenuProps> = props => {
    useScrollable()

    const escClick = useCallback(
        (event: KeyboardEvent) => {
            if (event.keyCode === 27) {
                props.onCloseHamburger()
            }
        },
        [props]
    )

    useEffect(() => {
        document.addEventListener('keydown', escClick, false)

        return () => {
            document.removeEventListener('keydown', escClick)
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    return (
        <div onClick={() => props.onCloseHamburger()} className={styles.background}>
            <div onClick={event => event.stopPropagation()} className={styles.container}>
                <ul className={styles.HamburgerModal}>
                    <Link
                        to={`/enode-frontend/${props.org}`}
                        style={{ textDecoration: 'none' }}
                    >
                        <li>Hjem</li>
                    </Link>
                    <Link to="/enode-frontend" style={{ textDecoration: 'none' }}>
                        <li>Bytt organisasjon</li>
                    </Link>
                    <Link
                        to={`/enode-frontend/${props.org}/stats`}
                        style={{ textDecoration: 'none' }}
                    >
                        <li>Statistikk</li>
                    </Link>
                </ul>
            </div>
        </div>
    )
}

export default HamburgerMenu
