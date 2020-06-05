import React, { useState } from 'react'
import { useParams } from 'react-router'
import Navbar from '../../common/Navbar'
import styles from './styles.module.css'
import HamburgerMenu from '../../common/HamburgerMenu'
import { Pie } from 'react-chartjs-2'
import { Line } from 'react-chartjs-2'

const Statistics: React.FC = () => {
    const { orgId } = useParams<{ orgId: string }>()
    const [isHamburgerOpen, setIsHamburgerOpen] = useState<boolean>(false)
    const fakeDataPieChart = {
        labels: ['Fullført', 'Under sending', 'Feil', 'Ingen data'],
        datasets: [
            {
                data: [18, 9, 3, 5],
                backgroundColor: [
                    'rgba(66, 230, 66, 0.699)',
                    'rgba(245, 245, 43, 0.815)',
                    'rgba(255, 99, 71, 0.74)',
                    'gray',
                ],
            },
        ],
    }

    const fakeDataLineChart = {
        labels: ['Mandag', 'Tirsdag', 'Onsdag', 'Torsdag', 'Fredag'],
        datasets: [
            {
                label: 'Fullført',
                data: [5, 5, 8, 9, 7],
                fill: false,
                borderColor: 'rgb(33, 194, 33)',
            },
            {
                label: 'Under sending',
                data: [3, 4, 6, 4, 5],
                fill: false,
                borderColor: 'yellow',
            },
            {
                label: 'Feil',
                data: [1, 1, 2, 3, 1],
                fill: false,
                borderColor: 'tomato',
            },
            {
                label: 'Ingen data',
                data: [3, 4, 4, 2, 4],
                fill: false,
                borderColor: 'gray',
            },
        ],
    }
    return (
        <>
            <Navbar
                filters={false}
                orgId={orgId}
                toggleHamburger={() => setIsHamburgerOpen(!isHamburgerOpen)}
                toggleFilter={() => ''}
                onSearch={() => ''}
                onFilterMessage={() => ''}
            />
            {isHamburgerOpen && (
                <HamburgerMenu
                    org={orgId}
                    onCloseHamburger={() => setIsHamburgerOpen(false)}
                />
            )}
            <div className={styles.container}>
                <h1 className={styles.header}>Statistikk</h1>
                <div className={styles.statistics}>
                    <div className={styles.pieChart}>
                        <Pie data={fakeDataPieChart} />
                    </div>
                    <div className={styles.lineChart}>
                        <Line
                            data={fakeDataLineChart}
                            options={{
                                scales: {
                                    yAxes: [
                                        {
                                            ticks: {
                                                suggestedMin: 0,
                                                suggestedMax: 10,
                                            },
                                            scaleLabel: {
                                                display: true,
                                                labelString: 'Meldinger',
                                            },
                                        },
                                    ],
                                    xAxes: [
                                        {
                                            scaleLabel: {
                                                display: true,
                                                labelString: 'Ukedag',
                                            },
                                        },
                                    ],
                                },
                            }}
                        />
                    </div>
                </div>
            </div>
        </>
    )
}

export default Statistics
