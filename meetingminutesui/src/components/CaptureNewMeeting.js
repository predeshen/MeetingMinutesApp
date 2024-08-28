import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const CaptureNewMeeting = () => {
    const [meetingTypes, setMeetingTypes] = useState([]);
    const [meetingTypeId, setMeetingTypeId] = useState('');
    const [date, setDate] = useState('');
    const [time, setTime] = useState('');
    const [previousOpenItems, setPreviousOpenItems] = useState([]);
    const [newMeetingItems, setNewMeetingItems] = useState([]);
    const [statusTypes, setStatusTypes] = useState([]);
    const [error, setError] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        axios.get('/api/meetings/meetingtypes')
            .then(response => setMeetingTypes(response.data))
            .catch(error => console.error(error));

        axios.get('/api/meetings/meetingitemstatustypes')
            .then(response => setStatusTypes(response.data))
            .catch(error => console.error(error));
    }, []);

    const handleMeetingTypeChange = (e) => {
        const selectedMeetingTypeId = e.target.value;
        setMeetingTypeId(selectedMeetingTypeId);

        // Fetch previous open items for the selected meeting type
        axios.get(`/api/meetings/previous-open-items/${selectedMeetingTypeId}`)
            .then(response => setPreviousOpenItems(response.data))
            .catch(error => console.error(error));
    };

    const handlePreviousItemStatusChange = (index, value) => {
        const items = [...previousOpenItems];
        items[index].status = value;
        setPreviousOpenItems(items);
    };

    const handleAddMeetingItem = () => {
        setNewMeetingItems([...newMeetingItems, { description: '', dueDate: '', personResponsible: '', status: '' }]);
    };

    const handleMeetingItemChange = (index, field, value) => {
        const items = [...newMeetingItems];
        items[index] = {
            ...items[index],
            [field]: value
        };
        setNewMeetingItems(items);
    };

    const handleSubmit = () => {
        if (!meetingTypeId || !date || !time || previousOpenItems.some(item => !item.status) || newMeetingItems.some(item => !item.status)) {
            setError('All fields are required.');
            return;
        }

        const request = {
            meetingTypeId,
            date,
            time,
            previousOpenItems: previousOpenItems.map(item => ({
                description: item.description,
                dueDate: item.dueDate,
                personResponsible: item.personResponsible,
                status: item.status
            })),
            newMeetingItems: newMeetingItems.map(item => ({
                description: item.description,
                dueDate: item.dueDate,
                personResponsible: item.personResponsible,
                status: item.status
            }))
        };

        axios.post('/api/meetings/captureNewMeeting', request)
            .then(response => {
                console.log(response.data);
                setError(''); // Clear any previous errors
                alert('Meeting capture successfully!'); // Add alert message
                navigate(`/meetingDetails/${response.data}`); // Navigate to the new meeting details
            })
            .catch(error => {
                console.error(error);
                alert('An error occurred while capturing the new meeting. Please try again.'); // Add alert message
                setError('An error occurred while capturing the new meeting. Please try again.');
            });
    };

    return (
        <div>
            <h2>Capture New Meeting</h2>
            {error && <div style={{ color: 'red' }}>{error}</div>}
            <div>
                <label>Meeting Type:</label>
                <select value={meetingTypeId} onChange={handleMeetingTypeChange}>
                    <option value="">Select a meeting type</option>
                    {meetingTypes.map(type => (
                        <option key={type.id} value={type.id}>{type.name}</option>
                    ))}
                </select>
            </div>
            <div>
                <label>Date:</label>
                <input type="date" value={date} onChange={e => setDate(e.target.value)} />
            </div>
            <div>
                <label>Time:</label>
                <input type="time" value={time} onChange={e => setTime(e.target.value)} />
            </div>
            <div>
                <h3>Previous Open Items</h3>
                {previousOpenItems.map((item, index) => (
                    <div key={index}>
                        <p>Description: {item.description}</p>
                        <p>Due Date: {item.dueDate}</p>
                        <p>Person Responsible: {item.personResponsible}</p>
                        <label>Status:</label>
                        <select value={item.status} onChange={e => handlePreviousItemStatusChange(index, e.target.value)}>
                            <option value="">Select a status</option>
                            {statusTypes.map(type => (
                                <option key={type.id} value={type.status}>{type.status}</option>
                            ))}
                        </select>
                    </div>
                ))}
            </div>
            <div>
                <h3>New Meeting Items</h3>
                {newMeetingItems.map((item, index) => (
                    <div key={index}>
                        <input
                            type="text"
                            placeholder="Description"
                            value={item.description}
                            onChange={e => handleMeetingItemChange(index, 'description', e.target.value)}
                        />
                        <input
                            type="date"
                            placeholder="Due Date"
                            value={item.dueDate}
                            onChange={e => handleMeetingItemChange(index, 'dueDate', e.target.value)}
                        />
                        <input
                            type="text"
                            placeholder="Person Responsible"
                            value={item.personResponsible}
                            onChange={e => handleMeetingItemChange(index, 'personResponsible', e.target.value)}
                        />
                        <label>Status:</label>
                        <select value={item.status} onChange={e => handleMeetingItemChange(index, 'status', e.target.value)}>
                            <option value="">Select a status</option>
                            {statusTypes.map(type => (
                                <option key={type.id} value={type.status}>{type.status}</option>
                            ))}
                        </select>
                    </div>
                ))}
                <button onClick={handleAddMeetingItem}>Add Meeting Item</button>
            </div>
            <button onClick={handleSubmit}>Submit</button>
        </div>
    );
};

export default CaptureNewMeeting;
