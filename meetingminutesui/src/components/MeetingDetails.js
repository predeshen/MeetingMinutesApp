import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

const MeetingDetails = () => {
    const { meetingId } = useParams();
    const [meeting, setMeeting] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    useEffect(() => {
        if (meetingId) {
            fetchMeetingDetails(meetingId);
        }
    }, [meetingId]);

    const fetchMeetingDetails = async (meetingId) => {
        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`/api/meetings/getMeeting/${meetingId}`);
            if (!response.ok) {
                throw new Error('Meeting not found');
            }

            const data = await response.json();
            console.log(data);
            setMeeting(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="container">
            <h1>Meeting Details</h1>

            {loading && <p>Loading...</p>}
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {meeting && (
                <div id="meetingDetails" className="meeting-details">
                    <h2>Meeting ID: {meeting.id}</h2>
                    <p><strong>Date:</strong> {meeting.date}</p>
                    <p><strong>Time:</strong> {meeting.time}</p>
                    <h3>Meeting Items</h3>
                    <ul>
                        {meeting.meetingItems.$values.map((item, index) => (
                            <li key={index} className="meeting-item">
                                <p><strong>Description:</strong> {item.description}</p>
                                <p><strong>Due Date:</strong> {item.dueDate}</p>
                                <p><strong>Person Responsible:</strong> {item.personResponsible}</p>
                            </li>
                        ))}
                    </ul>
                </div>
            )}
        </div>
    );
};

export default MeetingDetails;
