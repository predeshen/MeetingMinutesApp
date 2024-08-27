import React, { useEffect, useState } from 'react';
import axios from 'axios';

const MeetingLookups = () => {
    const [meetingTypes, setMeetingTypes] = useState([]);
    const [statusTypes, setStatusTypes] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchLookups = async () => {
            try {
                const [meetingTypesResponse, statusTypesResponse] = await Promise.all([
                    axios.get('/api/meetings/meetingtypes'),
                    axios.get('/api/meetings/meetingitemstatustypes')
                ]);

                setMeetingTypes(meetingTypesResponse.data);
                setStatusTypes(statusTypesResponse.data);
            } catch (error) {
                setError('There was an error fetching the lookup data.');
                console.error('Error fetching lookup data:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchLookups();
    }, []);

    if (loading) {
        return <p>Loading...</p>;
    }

    if (error) {
        return <p style={{ color: 'red' }}>{error}</p>;
    }

    return (
        <div>
            <h1>Meeting Lookups</h1>

            <h2>Meeting Types</h2>
            <ul>
                {meetingTypes.map(type => (
                    <li key={type.id}>{type.name}</li>
                ))}
            </ul>

            <h2>Meeting Item Status Types</h2>
            <ul>
                {statusTypes.map(status => (
                    <li key={status.id}>{status.status}</li>
                ))}
            </ul>
        </div>
    );
};

export default MeetingLookups;
