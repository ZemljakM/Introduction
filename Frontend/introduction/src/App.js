import './App.css';
import ClubTable from './ClubTable';
import PresidentTable from './PresidentTable';
import AddClub from './AddClub';
import AddPresident from './AddPresident';
import { useState, useEffect } from 'react';
import UpdateClub from './UpdateClub';
import axios from 'axios';
import ClubDetails from './ClubDetails';
import Paging from './Paging';
import Sorting from './Sorting';

function App() {
  const [list, setList] = useState([]);
  const [presidents, setPresidents] = useState([]);
  const [editClubId, setEditClubId] = useState(null);
  const [selectedClubId, setSelectedClubId] = useState(null);

  const [pageNumber, setPageNumber] = useState(1);
  const [totalClubs, setTotalClubs] = useState(0);
  const [sortingBy, setSortingBy] = useState('Name');

  useEffect(() => {
    getClubs();
    countClubs();
  }, [list, pageNumber, sortingBy]);


  async function getClubs(){
    axios.get("https://localhost:7056/api/Club", {params : {pageNumber:pageNumber, orderBy:sortingBy}})
      .then(response => { 
        setList(response.data); 
      })
      .catch(error => {
        console.error("Error fetching clubs: ", error);
      })
  }

  async function countClubs(){
    axios.get("https://localhost:7056/api/Club/Count")
      .then(response => { 
        setTotalClubs(response.data); 
      })
      .catch(error => {
        console.error("Error fetching clubs: ", error);
      })
  }


  useEffect(() => {
    if (list.length > 0) {
      localStorage.setItem('clubs', JSON.stringify(list));
    }
  }, [list]);


  const updateClub = (id) => {
      setEditClubId(id);
  };

  const selectClub = (id) => {
    setSelectedClubId(id);
  };

  const closeDetails = () => {
    setSelectedClubId(null);
  };

  const pageSize = 3;
  const totalPages = Math.ceil(totalClubs / pageSize);

  const handlePaging = (pageNumber) => {
    setPageNumber (pageNumber);
  };

  const handleSorting = (sortingBy) => {
    setSortingBy (sortingBy);
  };

  async function deleteClub(clubId){
    try{
      const response = await axios.delete("https://localhost:7056/api/Club" + `/${clubId}`);
      if(response.status === 200){
          alert("Successfully deleted.");
      }
    }
    catch(error){
        alert(error.message);
    }
  }


  return (
    <div>
      <h1>Club List</h1>
      <Sorting sortingBy={sortingBy} handleSorting={handleSorting}/>
      <Paging pageNumber={pageNumber} totalPages={totalPages} handlePaging={handlePaging}/>
      <ClubTable clubs={list} deleteClub={deleteClub} updateClub={updateClub} selectClub={selectClub}/>
      {selectedClubId && <ClubDetails clubId={selectedClubId} closeDetails={closeDetails}/>}
      {editClubId 
        ? <UpdateClub clubs={list} setList={setList} clubId={editClubId} setEditClubId={setEditClubId}/>
        : <AddClub clubs={list} setList={setList} />
      }
      <h1>President List</h1>
      <PresidentTable presidents={presidents} />
      <AddPresident presidents={presidents} setPresidents={setPresidents} />
    </div>
  );
}

export default App;
