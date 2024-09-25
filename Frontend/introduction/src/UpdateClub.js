import React from "react";
import { useState } from "react";
import Button from './Button';
import './AddClub.css';
import axios from 'axios';


function UpdateClub({clubs, setList, clubId, setEditClubId}){
    const clubToUpdate = clubs.find(club => club.id === clubId);
    const [club, setClub] = useState({...clubToUpdate});

    function handleChange(e){
        setClub({...club, [e.target.name] : e.target.value});
    }


    async function handleSubmit(e){
        e.preventDefault();
        try{
            const response = await axios.put("https://localhost:7056/api/Club" + `/${clubId}`, club);
            if(response.status === 200){
                alert("Successfully updated.");
                setEditClubId(null);
            }
        }
        catch(error){
            alert(error.message);
        }
    }

    return (
        <div id="updateForm">
            <form>
                <div>
                    <label>Name: </label>
                    <input 
                        type = "text"
                        name = "name"
                        value = {club.name}
                        onInput = {handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Sport: </label>
                    <select name = "sport" onInput = {handleChange} required>
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
                        value = {club.dateOfEstablishment}
                        onInput = {handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Number of members: </label>
                    <input 
                        type = "number"
                        name = "numberOfMembers"
                        value = {club.numberOfMembers}
                        onInput = {handleChange}
                        required
                    />
                </div>
                <div>
                    <label>President: </label>
                    <input 
                        type = "text"
                        name = "clubPresidentId"
                        value = {club.clubPresidentId}
                        onInput = {handleChange}
                        required
                    />
                </div>
                <Button className="updateClub" text="Save changes" onClick={handleSubmit} />
            </form>
        </div>
    );
}

export default UpdateClub;