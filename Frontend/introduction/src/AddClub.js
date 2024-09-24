import React from "react";
import { useState } from "react";
import './AddClub.css';
import Button from './Button';

function AddClub({clubs, setList}){
    const [club, setClub] = useState({});

    function handleChange(e){
        console.log(club);
        setClub({...club, [e.target.name] : e.target.value});
    }

    function handleSubmit(e){
        e.preventDefault();
        const newId = clubs.length > 0 ? Math.max(...clubs.map(c => c.id)) : 0;
        const newClub = {...club, id: newId + 1};
        setList([...clubs, newClub]);  
        setClub({name: "", sport: "", dateOfEstablishment: "", members: "", president: "" });
    }

    return (
        <div id="createForm">
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Name: </label>
                    <input 
                        type = "text"
                        name = "name"
                        value = {club.name || ''}
                        onInput = {handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Sport: </label>
                    <select name = "sport" defaultValue="" onInput = {handleChange} required>
                        <option value="" disabled hidden>Select a sport</option>
                        <option value="Football">Football</option>
                        <option value="Handball">Handball</option>
                        <option value="Basketball">Basketball</option>
                        <option value="Tenis">Tenis</option>
                        <option value="Other">Other</option>
                    </select>
                </div>
                <div>
                    <label>Date of establishment: </label>
                    <input 
                        type = "date"
                        name = "dateOfEstablishment"
                        value = {club.dateOfEstablishment || ''}
                        onInput = {handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Number of members: </label>
                    <input 
                        type = "number"
                        name = "members"
                        value = {club.members || ''}
                        onInput = {handleChange}
                        required
                    />
                </div>
                <div>
                    <label>President: </label>
                    <input 
                        type = "text"
                        name = "president"
                        value = {club.president || ''}
                        onInput = {handleChange}
                        required
                    />
                </div>
                <Button className="addClub" text="Create club" />
            </form>
        </div>
    );
}

export default AddClub;