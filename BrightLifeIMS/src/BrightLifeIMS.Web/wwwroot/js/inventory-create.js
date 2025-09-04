/ src/BrightLifeIMS.Web/wwwroot/js/inventory-create.js
// Custom ID Format Builder
function addToFormat(component) {
    const formatInput = document.getElementById('customIdFormat');
    
    if (component === 'ITEM-') {
        // For fixed text, prompt user
        const text = prompt('Enter fixed text:', 'ITEM');
        if (text) {
            formatInput.value += text + '-';
        }
    } else {
        formatInput.value += component;
    }
    
    updatePreview();
}

function updatePreview() {
    const format = document.getElementById('customIdFormat').value;
    const preview = document.getElementById('idPreview');
    
    if (!format) {
        preview.textContent = 'ITEM-000001';
        return;
    }
    
    // Simple preview generation
    let previewText = format;
    previewText = previewText.replace('{SEQUENCE:000000}', '000001');
    previewText = previewText.replace('{SEQUENCE:000001}', '000001');
    previewText = previewText.replace('{YEAR}', new Date().getFullYear());
    previewText = previewText.replace('{RANDOM:6}', '123456');
    previewText = previewText.replace('{GUID:8}', 'A1B2C3D4');
    
    preview.textContent = previewText;
}

// Auto-save functionality (simplified version)
class SimpleAutoSave {
    constructor(formId) {
        this.form = document.getElementById(formId);
        this.indicator = document.getElementById('autoSaveIndicator');
        this.saveInterval = 8000; // 8 seconds
        this.isModified = false;
        
        if (this.form) {
            this.initialize();
        }
    }
    
    initialize() {
        // Monitor form changes
        this.form.addEventListener('input', () => this.markAsModified());
        this.form.addEventListener('change', () => this.markAsModified());
        
        // Start auto-save timer
        setInterval(() => {
            if (this.isModified) {
                this.saveToLocalStorage();
            }
        }, this.saveInterval);
        
        // Load any saved data on page load
        this.loadFromLocalStorage();
    }
    
    markAsModified() {
        this.isModified = true;
        this.updateIndicator('modified');
    }
    
    saveToLocalStorage() {
        const formData = new FormData(this.form);
        const data = {};
        
        formData.forEach((value, key) => {
            data[key] = value;
        });
        
        localStorage.setItem('inventoryDraft', JSON.stringify(data));
        this.isModified = false;
        this.updateIndicator('saved');
        
        console.log('Draft saved to local storage');
    }
    
    loadFromLocalStorage() {
        const savedData = localStorage.getItem('inventoryDraft');
        if (savedData) {
            try {
                const data = JSON.parse(savedData);
                
                // Restore form values
                Object.keys(data).forEach(key => {
                    const field = this.form.elements[key];
                    if (field) {
                        if (field.type === 'checkbox') {
                            field.checked = data[key] === 'true' || data[key] === true;
                        } else {
                            field.value = data[key];
                        }
                    }
                });
                
                this.updateIndicator('restored');
                console.log('Draft restored from local storage');
            } catch (e) {
                console.error('Error restoring draft:', e);
            }
        }
    }
    
    updateIndicator(status) {
        if (!this.indicator) return;
        
        const indicators = {
            'modified': '<i class="fas fa-circle text-warning"></i> Modified',
            'saved': '<i class="fas fa-check-circle text-success"></i> Saved locally',
            'restored': '<i class="fas fa-history text-info"></i> Draft restored'
        };
        
        this.indicator.innerHTML = indicators[status] || '<i class="fas fa-circle"></i> Ready';
    }
}

// Initialize on page load
document.addEventListener('DOMContentLoaded', function() {
    // Initialize auto-save for inventory form
    if (document.getElementById('inventoryForm')) {
        new SimpleAutoSave('inventoryForm');
    }
    
    // Initialize ID format preview
    const formatInput = document.getElementById('customIdFormat');
    if (formatInput) {
        formatInput.addEventListener('input', updatePreview);
        updatePreview(); // Initial preview
    }
    
    // Enable Bootstrap tooltips
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
});