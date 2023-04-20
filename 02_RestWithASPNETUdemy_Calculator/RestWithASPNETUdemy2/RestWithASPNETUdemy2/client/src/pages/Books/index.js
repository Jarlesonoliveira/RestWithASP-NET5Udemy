import React, { useState} from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { FiPower, FiEdit, FiTrash2 } from 'react-icons/fi';
import api from '../../services/api';
import './styles.css';
import logoImage from '../../assets/logo.svg';

export default function Books() {

    const [books, setBooks] = useState([]);
    const [page, setPage] = useState(0);

    const userName = localStorage.getItem('userName');
    const accessToken = localStorage.getItem('accessToken');

    const authorization = {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    };

    const navigate = useNavigate();

    React.useEffect(() => {        
    }, [accessToken]);

    async function fetchMoreBooks(){
        const response = await api.get(`api/Book/v1?page=${page}$limite=4&direction=asc`, authorization );
        setBooks([ ...books, ...response.data._embedded.bookVOes]);
        setPage(page + 1); 
    }

    async function editBook(id){
        try {
            navigate(`/book/new/${id}`)
        } catch (error) {
            alert('Delete failed! Try again!')
        }
    }

    async function logout(){
        try {
            await api.get(`api/auth/v1/revoke`, authorization);
            localStorage.clear();
            navigate('/');
        } catch (error) {
            alert('Logout failed! Try again!')
        }
    }

    async function deleteBook(id){
        try {
            await api.delete(`api/Book/v1/${id}`,authorization);
            setBooks(books.filter(book => book.id !== id))
        } catch (error) {
            alert('Delete failed! Try again!')
        }
    }

    return (
        <div className="Book-container">
            <header>
                <img src={logoImage} alt="Erudio"/>
                <span>Welcome, <strong>{userName.toUpperCase()}</strong>!</span>
                <Link className="button" to="/book/new/0">Add New Book</Link>
                <button onClick={logout} type="button">
                    <FiPower size={18} color="#251FC5" />
                </button>
            </header>

            <h1>Registered Books</h1>
            <ul>
                {books.map(book =>(
                    <li key={book.id}>
                    <strong>Title:</strong>
                    <p>{book.title}</p>
                    <strong>Author:</strong>
                    <p>{book.author}</p>
                    <strong>Price:</strong>
                    <p>{Intl.NumberFormat('pt-BR', {style: 'currency', currency: 'BRL'}).format(book.price)}</p>
                    <strong>Release Date:</strong>
                    <p>{Intl.NumberFormat('pt-BR').format(new Date(book.launchDate))}</p>

                    <button onClick={()=> editBook(book.id)} type="button">
                        <FiEdit size={20} color="#251FC5"/>
                    </button>

                    <button onClick={() => deleteBook(book.id)} type="button">
                        <FiTrash2 size={20} color="#251FC5"/>
                    </button>
                </li>
                ))}                         
            </ul>
            <button className='button' onClick={fetchMoreBooks} type='button'>Load More</button>
        </div>
    );
}