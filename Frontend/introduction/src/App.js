import './App.css';
import ClubTable from './ClubTable';
import AddClub from './AddClub';
import { useState, useEffect } from 'react';
import UpdateClub from './UpdateClub';

function App() {
  const [list, setList] = useState([]);
  const [editClubId, setEditClubId] = useState(null);

  useEffect(() => {
    const storedClubs = JSON.parse(localStorage.getItem('clubs')) || [];
    setList(storedClubs);
  }, []);

  useEffect(() => {
    localStorage.setItem('clubs', JSON.stringify(list));
  }, [list]);


  const updateClub = (id) => {
      setEditClubId(id);
  };

  const deleteClub = (id) => {
    setList(list.filter(club => club.id !== id));
    if (editClubId === id) {
      setEditClubId(null);
    }
  };

  return (
    <div>
      <h1>Club List</h1>
      <ClubTable clubs={list} deleteClub={deleteClub} updateClub={updateClub} />
      {editClubId 
        ? <UpdateClub clubs={list} setList={setList} clubId={editClubId} setEditClubId={setEditClubId}/>
        : <AddClub clubs={list} setList={setList} />
      }
      
    </div>
  );
}

export default App;
