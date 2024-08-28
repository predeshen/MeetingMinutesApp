import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import CaptureNewMeeting from './components/CaptureNewMeeting';
import UpdateMeetingItemStatus from './components/UpdateMeetingItemStatus';
import MeetingDetails from './components/MeetingDetails';
import NavBar from './components/NavBar'; 
import Home from './components/Home'; 


function App() {
    return (
        <Router>
            <div>
                <NavBar />
                <Routes>
                    <Route path="/" element={<Navigate to="/Home" />} />
                    <Route path="/Home" element={<Home />} />
                    <Route path="/capture-new-meeting" element={<CaptureNewMeeting />} />
                    <Route path="/update-meeting-item-status" element={<UpdateMeetingItemStatus />} />
                    <Route path="/meetingDetails/:meetingId" element={<MeetingDetails />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
