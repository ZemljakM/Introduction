import ClubRow from './ClubRow';
import './ClubTable.css';

function ClubTable({ clubs, deleteClub, updateClub }) {
    return (
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Sport</th>
            <th>Date of establishment</th>
            <th>Members</th>
            <th>President</th>
          </tr>
        </thead>
        <tbody>{clubs.map((club) => (<ClubRow key={club.id} club={club} deleteClub={deleteClub} updateClub={updateClub}/>))}</tbody>
      </table>
    );
  }


export default ClubTable;