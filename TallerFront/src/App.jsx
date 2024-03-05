import { BrowserRouter, Routes, Route } from 'react-router-dom'
import './App.css'
import Home from './screens/Home'
import Navbar from './components/Navbar'
import NotFound from './screens/NotFound'
import Productos from './screens/Productos'

function App() {


  return (
    <>  
     <BrowserRouter>
      <Navbar/>
      <Routes>
        <Route path='/' element={<Home/>}/>
        <Route path='/*' element={<NotFound/>}/>
        <Route path='/productos' element={<Productos/>}/>
      </Routes>


    </BrowserRouter>
  
    </>
  )
}

export default App
