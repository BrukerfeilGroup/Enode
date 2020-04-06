import React from 'react'
import Message from '../../types/Message'
import { parseDate } from '../../utils/utils'
import styles from './styles.module.css'

type TimelineProps = {
    message: Message
}

const Timeline: React.FC<TimelineProps> = props => {
    return (
        <ul className={styles.timeLine}>
            {props.message.difiMessage
                ? props.message.difiMessage.messageStatuses.map((s, i) => {
                      const dateLastUpdate = new Date(s.lastUpdate)
                      return (
                          <React.Fragment key={s.id}>
                              <li>
                                  <span
                                      className={
                                          i === 0
                                              ? styles.firstTimeLineItem
                                              : styles.timeLineItem
                                      }
                                  >
                                      {s.status}
                                  </span>
                                  <br />
                                  <div className={styles.lastUpdated}>
                                      {parseDate(dateLastUpdate)}
                                  </div>
                                  <br />
                                  <br />
                              </li>
                          </React.Fragment>
                      )
                  })
                : null}
        </ul>
    )
}

export default Timeline
