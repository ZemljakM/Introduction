import React from "react";
import { useState } from "react";
import './AddClub.css';
import Button from './Button';
import axios from 'axios';

function AddClub({clubs, setList}){
    const [club, setClub] = useState({});

    function handleChange(e){
        console.log(club);
        setClub({...club, [e.target.name] : e.target.value});
    }

    async function handleSubmit(e){
        e.preventDefault();
        try{
            const response = await axios.post("https://localhost:7056/api/Club", club);
            if(response.status === 200){
                setClub({name: "", sport: "", dateOfEstablishment: "", numberOfMembers: "", clubPresidentId: "" });
            }
        }
        catch(error){
            alert(error.message);
        }
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
                        name = "numberOfMembers"
                        value = {club.numberOfMembers || ''}
                        onInput = {handleChange}
                        required
                    />
                </div>
                <div>
                    <label>President: </label>
                    <input 
                        type = "text"
                        name = "clubPresidentId"
                        value = {club.clubPresidentId || ''}
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