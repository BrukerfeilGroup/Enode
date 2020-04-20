import React from 'react'

type NoMessagesRowProps = {
    cols: number
    direction: string
}

const NoMessagesRow: React.FC<NoMessagesRowProps> = props => {
    return (
        <tr style={{ textAlign: 'center', height: '500px' }}>
            <td colSpan={props.cols} style={{ fontSize: 'x-large' }}>
                Fant ingen {props.direction === 'in' ? 'innkommende' : 'utgående'}
                meldinger
            </td>
        </tr>
    )
}

export default NoMessagesRow
