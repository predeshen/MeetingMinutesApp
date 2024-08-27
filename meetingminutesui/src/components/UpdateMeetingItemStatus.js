import React, { useState } from 'react';
import axios from 'axios';

const UpdateMeetingItemStatus = () => {
    const [meetingItemId, setMeetingItemId] = useState('');
    const [status, setStatus] = useState('');

    const handleSubmit = () => {
        const request = { status };

        axios.put(`/api/meetingitems/${meetingItemId}/status`, request)
            .then(response => console.log(response.data))
            .catch(error => console.error(error));
    };

    return (
        <div>
            <h2>Update Meeting Item Status</h2>
            <div>
                <label>Meeting Item ID:</label>
                <input type="text" value={meetingItemId} onChange={e => setMeetingItemId(e.target.value)} />
            </div>
            <div>
                <label>Status:</label>
                <input type="text" value={status} onChange={e => setStatus(e.target.value)} />
            </div>
            <button onClick={handleSubmit}>Submit</button>
        </div>
    );
};

export default UpdateMeetingItemStatus;
