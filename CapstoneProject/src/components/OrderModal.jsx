// OrderModal.js

import React, { useState } from 'react';
import Modal from 'react-modal';
import { Button, Slider } from 'semantic-ui-react';

Modal.setAppElement('#root');

const OrderModal = ({ isOpen, onClose, onOrder }) => {
    const [selectedProducts, setSelectedProducts] = useState([]);
    const [numOfProducts, setNumOfProducts] = useState(1); // Başlangıçta 1 ürün seçili olacak
    const [selectedOption, setSelectedOption] = useState('all');

    const handleOrder = () => {
        onOrder(selectedProducts, numOfProducts, selectedOption);
        onClose();
    };

    const handleOptionChange = (option) => {
        setSelectedOption(option);
        // Seçeneklere göre ürünleri seçin veya işlemleri yapın
        switch (option) {
            case 'discounted':
                // İndirimli ürünleri seçin veya işlemleri yapın
                break;
            case 'non-discounted':
                // İndirimsiz ürünleri seçin veya işlemleri yapın
                break;
            case 'all':
                // Tüm ürünleri seçin veya işlemleri yapın
                break;
            default:
                break;
        }
    };

    return (
        <Modal isOpen={isOpen} onRequestClose={onClose} contentLabel="Sipariş Ver">
            <h2>Sipariş Ver</h2>
            <div>
                <label>Hangi ürünleri istersin?</label>
                <div>
                    <input
                        type="radio"
                        value="discounted"
                        checked={selectedOption === 'discounted'}
                        onChange={() => handleOptionChange('discounted')}
                    />
                    <label>İndirimli Ürünler</label>
                </div>
                <div>
                    <input
                        type="radio"
                        value="non-discounted"
                        checked={selectedOption === 'non-discounted'}
                        onChange={() => handleOptionChange('non-discounted')}
                    />
                    <label>İndirimsiz Ürünler</label>
                </div>
                <div>
                    <input
                        type="radio"
                        value="all"
                        checked={selectedOption === 'all'}
                        onChange={() => handleOptionChange('all')}
                    />
                    <label>Hepsi</label>
                </div>
            </div>
            <div>
                <label>Kaç tane ürün listelemek istersin?</label>
                <Slider
                    value={numOfProducts}
                    onChange={(e, data) => setNumOfProducts(data.value)}
                    min={1}
                    max={10}
                    step={1}
                    valueLabelDisplay="auto"
                />
            </div>
            <Button onClick={handleOrder} primary>Onayla</Button>
        </Modal>
    );
};

export default OrderModal;
