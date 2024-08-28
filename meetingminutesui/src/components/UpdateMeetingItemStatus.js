import React, { useState, useEffect } from 'react';
import axios from 'axios';

const UpdateMeetingItemStatus = () => {
    const [meetings, setMeetings] = useState([]);
    const [selectedMeetingId, setSelectedMeetingId] = useState('');
    const [meetingItems, setMeetingItems] = useState([]);
    const [selectedMeetingItemId, setSelectedMeetingItemId] = useState('');
    const [statusTypes, setStatusTypes] = useState([]);
    const [status, setStatus] = useState('');
    const [error, setError] = useState('');

    useEffect(() => {
        // Fetch meetings
        axios.get('/api/meetings/meetings')
            .then(response => setMeetings(response.data))
            .catch(error => console.error(error));

        // Fetch status types
        axios.get('/api/meetings/meetingitemstatustypes')
            .then(response => setStatusTypes(response.data))
            .catch(error => console.error(error));
    }, []);

    const handleMeetingChange = (e) => {
        const meetingId = e.target.value;
        setSelectedMeetingId(meetingId);

        // Fetch meeting items for the selected meeting
        axios.get(`/api/meetings/${meetingId}/items`)
            .then(response => setMeetingItems(response.data))
            .catch(error => console.error(error));
    };

    const handleSubmit = () => {
        if (!selectedMeetingItemId || !status) {
            setError('Please select a meeting item and a status.');
            return;
        }

        const request = { status };

        axios.put(`/api/meetings/meetingitems/${selectedMeetingItemId}/status`, request)
            .then(response => {
                console.log(response.data);
                setError(''); // Clear any previous errors
            })
            .catch(error => {
                console.error(error);
                setError('An error occurred while updating the meeting item status.');
            });
    };

    return (
        <div>
            <h2>Update Meeting Item Status</h2>
            {error && <div style={{ color: 'red' }}>{error}</div>}
            <div>
                <label>Meeting:</label>
                <select value={selectedMeetingId} onChange={handleMeetingChange}>
                    <option value="">Select a meeting</option>
                    {meetings.map(meeting => (
                        <option key={meeting.id} value={meeting.id}>{meeting.date}</option>
                    ))}
                </select>
            </div>
            <div>
                <label>Meeting Item:</label>
                <select value={selectedMeetingItemId} onChange={e => setSelectedMeetingItemId(e.target.value)}>
                    <option value="">Select a meeting item</option>
                    {meetingItems.map(item => (
                        <option key={item.id} value={item.id}>{item.description}</option>
                    ))}
                </select>
            </div>
            <div>
                <label>Status:</label>
                <select value={status} onChange={e => setStatus(e.target.value)}>
                    <option value="">Select a status</option>
                    {statusTypes.map(type => (
                        <option key={type.id} value={type.status}>{type.status}</option>
                    ))}
                </select>
            </div>
            <button onClick={handleSubmit}>Submit</button>
        </div>
    );
};

export default UpdateMeetingItemStatus;
