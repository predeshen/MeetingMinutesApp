import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import CaptureNewMeeting from './components/CaptureNewMeeting';
import UpdateMeetingItemStatus from './components/UpdateMeetingItemStatus';

function App() {
    return (
        <Router>
            <div>
                <Routes>
                    <Route path="/" element={<CaptureNewMeeting />} />
                    <Route path="/capture-new-meeting" element={<CaptureNewMeeting />} />
                    <Route path="/update-meeting-item-status" element={<UpdateMeetingItemStatus />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
