import React from 'react'
import useScrollable from '../../hooks/useScrollable'
import { Link } from 'react-router-dom'
import styles from './styles.module.css'

type HamburgerMenuProps = {
    org: string
}

const HamburgerMenu: React.FC<HamburgerMenuProps> = props => {
    useScrollable()

    return (
        <div className={styles.container}>
            <ul className={styles.HamburgerModal}>
                <Link to="" style={{ textDecoration: 'none' }}>
                    <li>Bytt organisasjon</li>
                </Link>
                <Link to={`${props.org}/stats`} style={{ textDecoration: 'none' }}>
                    <li>Statistikk</li>
                </Link>
                <Link to={props.org} style={{ textDecoration: 'none' }}>
                    <li>Meldinger</li>
                </Link>
            </ul>
        </div>
    )
}

export default HamburgerMenu
