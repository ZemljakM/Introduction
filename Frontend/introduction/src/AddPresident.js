import React from "react";
import { useState } from "react";
import './AddClub.css';
import Button from './Button';
import axios from 'axios';

function AddPresident({presidents, setPresidents}){
    const [president, setPresident] = useState({});

    function handleChange(e){
        setPresident({...president, [e.target.name] : e.target.value});
    }

    async function handleSubmit(e){
        e.preventDefault();
        try{
            const response = await axios.post("https://localhost:7056/api/ClubPresident", president);
            if(response.status === 200){
                setPresidents([...presidents, {id: response.data, ...president}])
                setPresident({firstName: "", lastName: ""});
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
                    <label>First name: </label>
                    <input 
                        type = "text"
                        name = "firstName"
                        value = {president.firstName || ''}
                        onInput = {handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Last name: </label>
                    <input 
                        type = "text"
                        name = "lastName"
                        value = {president.lastName || ''}
                        onInput = {handleChange}
                        required
                    />
                </div>
                <Button className="addPresident" text="Create president" />
            </form>
        </div>
    );
}

export default AddPresident;