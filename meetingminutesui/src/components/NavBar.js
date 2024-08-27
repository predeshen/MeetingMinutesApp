import React from 'react';
import { Link } from 'react-router-dom'; // Assuming you're using react-router for navigation

const NavBar = () => {
    return (
        <nav style={styles.nav}>
            <ul style={styles.navList}>
                <li style={styles.navItem}><Link to="/Home" style={styles.navLink}>Home</Link></li>
                <li style={styles.navItem}><Link to="/capture-new-meeting" style={styles.navLink}>Capture new meeting</Link></li>
                <li style={styles.navItem}><Link to="/update-meeting-item-status" style={styles.navLink}>Update items</Link></li>
            </ul>
        </nav>
    );
};

const styles = {
    nav: {
        backgroundColor: '#333',
        padding: '10px 20px',
    },
    navList: {
        listStyleType: 'none',
        margin: 0,
        padding: 0,
        display: 'flex',
        justifyContent: 'space-around',
    },
    navItem: {
        margin: '0 10px',
    },
    navLink: {
        color: '#fff',
        textDecoration: 'none',
    }
};

export default NavBar;
