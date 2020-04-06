import { useEffect } from 'react'

const useScrollable = () => {
    useEffect(() => {
        //Makes page not scrollable when Hamburger
        document.body.style.overflow = 'hidden'

        //Makes page scrollable when hamburgermenu is unmounted
        return () => {
            document.body.style.overflow = 'unset'
        }
    }, [])
}

export default useScrollable
