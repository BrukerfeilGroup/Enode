import React from 'react'
import Message from '../../types/Message'
import { parseDate } from '../../utils/utils'
import styles from './styles.module.css'

type TimelineProps = {
    message: Message
}

const Timeline: React.FC<TimelineProps> = props => {
    return (
        <div className={styles.timeLineContainer}>
            <ul className={styles.timeLine}>
                {props.message.difiMessage
                    ? props.message.difiMessage.messageStatuses.map((s, i) => {
                          const dateLastUpdate = new Date(s.lastUpdate)
                          const lastIndex =
                              props.message.difiMessage.messageStatuses.length - 1
                          return (
                              <React.Fragment key={s.id}>
                                  <li>
                                      <span
                                          className={
                                              i === lastIndex
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
                                  </li>
                              </React.Fragment>
                          )
                      })
                    : null}
            </ul>
        </div>
    )
}

export default Timeline
