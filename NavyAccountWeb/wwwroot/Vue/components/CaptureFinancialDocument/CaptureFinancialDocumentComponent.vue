<template>

    <div>
        <div class="card-body">



            <div class="row">
                <div class="col-12 col-xl-4">
                    <div class="form-group">
                        <label class="form-label">Document Type</label>
                        <select class="form-control" name="documentType" v-model="documentType" required>
                            <option value="Receipt">Receipt</option>
                            <option value="Journal">Journal</option>
                            <option value="Payment">Payment</option>
                        </select>
                    </div>
                </div>
                <div class="col-12 col-xl-4">
                    <div class="form-group">
                        <label class="form-label">Document No</label>
                        <input type="text" name="documentNo" v-model="documentNo" class="form-control" required readonly />
                    </div>
                </div>
                <div class="col-12 col-xl-4">
                    <div class="form-group">
                        <label class="form-label">Reference No</label>
                        <input class="form-control" v-model="referenceNo" placeholder="Reference No" />
                    </div>
                </div>
                <div class="col-12 col-md-4">
                    <label>Document Date</label>
                    <vuejsDatepicker v-model="documentDate" input-class="form-control" name="documentDate"></vuejsDatepicker>
                </div>

            </div>
            <div class="row">
                <div class="col-6 col-xl-4">
                    <div class="btn-group mr-2 sw-btn-group-extra" role="group"><button class="btn btn-submit btn-primary" @click="getDocumentNo" type="button" :disabled="!((totalCredit - totalDebit) == 0 && (totalCredit + totalDebit) != 0)"> Accept </button></div>
                </div>
                <div class="col-6 col-xl-4">
                    <div class="btn-group mr-2 sw-btn-group-extra" role="group"><button class="btn btn-submit btn-primary" type="button" @click="submitReceipt" :disabled="!((totalCredit - totalDebit) == 0 && (totalCredit + totalDebit) != 0 && documentNo != '')"> {{ processingorsave }} </button></div>
                </div>

                <div class="col-6 col-xl-4">
                    <div class="btn-group mr-2 sw-btn-group-extra" role="group">
                        <button v-show="!editIndex" @click="add" class="btn btn-submit btn-primary">Add item</button>
                        </div>
                    </div>

                </div>


            <table class="table table-bordered mt-4">
                <thead class="thead-light">
                    <tr>
                        <th width="1%">#</th>
                        <th width="35%">Account Code</th>
                        <th width="17%">Debit Amount</th>
                        <th width="17%">Credit Amount</th>
                        <th width="25%">Remarks</th>
                        <th width="8%">Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in items" :key="index">
                        <td>{{ index + 1 }}</td>
                        <td>
                            <span v-if="editIndex !== index">{{ item.code }}</span>
                            <span v-if="editIndex === index">
                                <select class="form-control" v-model="item.code" name="code" required>
                                    <option v-for="coa in chartofAccountList" v-bind:value="coa.acctcode" v-bind:key="coa.acctcode"> {{ coa.description }} </option>
                                </select>
                            </span>
                        </td>
                        
                        <td>
                            <span v-if="editIndex !== index">{{ item.debitAmount }}</span>
                            <span v-if="editIndex === index">
                                <input class="form-control " type="number" v-model.number="item.debitAmount">
                            </span>
                        </td>
                        <td>
                            <span v-if="editIndex !== index">{{ item.creditAmount }}</span>
                            <span v-if="editIndex === index">
                                <input class="form-control " type="number" v-model.number="item.creditAmount">
                            </span>
                        </td>
                        <td>
                            <span v-if="editIndex !== index">{{ item.remarks }}</span>

                            <span v-if="editIndex === index">
                                <input class="form-control " v-model="item.remarks">
                            </span>
                        </td>

                        <!--<td><div class="text-right">{{ subtotal(item) | money }}</div></td>-->
                        <td>
                            <span v-if="editIndex !== index">

                                <button @click="edit(item, index)" class="btn btn-sm btn-outline-secondary mr-2">Edit</button>
                                <button @click="remove(item, index)" class="btn btn-sm btn-outline-secondary mr-2">Remove</button>
                            </span>
                            <span v-else>
                                <button class="btn btn-sm btn-outline-secondary mr-2" @click="cancel(item)">Cancel</button>
                                <button class="btn btn-sm btn-outline-secondary mr-2" @click="save(item)">Save</button>
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>


            <div class="col-12">

                <!--<div class="input-group input-group-sm mb-3">
            <div class="input-group-prepend">
                <span class="input-group-text">Sub total</span>
            </div>
            <input class="form-control form-control-sm text-right" disabled :value="this.allSubTotal | money">
        </div>

        <div class="input-group input-group-sm mb-3">
            <div class="input-group-prepend">
                <span class="input-group-text">Tax</span>
            </div>
            <input class="form-control form-control-sm" type="number" v-model.number="tax">
            <div class="input-group-append">
                <span class="input-group-text">%</span>
            </div>
        </div>-->

                <div class="input-group input-group-lg mb-3">

                    <div class="input-group-prepend">
                        <span class="input-group-text">Debit Amount</span>
                    </div>
                    <input class="form-control form-control-sm text-right" disabled :value="this.totalDebit | money">



                    <div class="input-group-prepend">
                        <span class="input-group-text">Credit Amount</span>
                    </div>
                    <input class="form-control form-control-sm text-right" disabled :value="this.totalCredit | money">



                    <div class="input-group-prepend">
                        <span class="input-group-text">Total</span>
                    </div>
                    <input class="form-control form-control-sm text-right" disabled :value="(this.totalCredit - this.totalDebit) | money">

                </div>

            </div>




        </div>

    </div>


</template>

<script>
    import vuejsDatepicker from 'vuejs-datepicker';
    export default {
        props:['fundtypecode'],
         components: {
            vuejsDatepicker
        },
        filters: {
            money: (value) => new Intl.NumberFormat().format(value)
        },
        
        data() {
            return {
                editIndex: null,
                originalData: null,
                documentNo: '',
                processingorsave:'Save',
                chartofAccountsRemoved: [],
                chartofAccountsStandBy : [],
                referenceNo: '',
                documentType: '',
                documentDate:'',
                chartofAccountList: null,
                items: [
                   // { code: '111',  description: 'Cuka mahal', qty: 1, unit: null, price: 3500, discount: 10, }
                ]
            };
        },
        created() {
            this.$store.state.objectToUpdate = null
        },
        computed: {

            totalCredit() {
                return this.items
                    .map(item => item.creditAmount)
                    .reduce((a, b) => a + b, 0)
            },
            totalDebit() {
                return this.items
                    .map(item => item.debitAmount)
                    .reduce((a, b) => a + b, 0)
            }

        },
        mounted() {
            axios
                .get('/api/ChartofAccount/getAllChartofAccounts')
                .then(response => {

                    this.chartofAccountList = response.data
                    this.chartofAccountsStandBy = response.data

                })
                
        },
        methods: {

            add() {
               
                this.originalData = null
                this.items.push({ code: '', debitAmount: 0, creditAmount: 0, remarks: '' })
                this.editIndex = this.items.length - 1
            },

            edit(item, index) {
                
                let chatofAccountItem = this.chartofAccountsStandBy.filter(function (el) {
                    return el.acctcode === item.code;
                })
                
                this.chartofAccountList.push(chatofAccountItem[0]);
                this.originalData = Object.assign({}, item)
                this.editIndex = index
            },
            pad(n,width,z){
            z = z || '0';
            n = n + '';
            return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
            },
            getDocumentNo() {
                if (!this.documentType) {
                    alert("Pick a Document Type");
                    return;
                }
                else {
                    axios
                        .get(`/api/FundTypeCode/getFundTypeByCode/${this.fundtypecode}`)
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            let receiptNumber = '';
                            if (response.data.responseCode == '200') {
                                if (this.documentType == "Receipt") {
                                    if (response.data.data.receiptno == 0) {
                                        let lastNo = (new Date()).getFullYear() + "0001";
                                        receiptNumber = "R" + lastNo;
                                    }
                                    else {

                                        let lastNo = (parseInt(response.data.data.receiptno) + 1);
                                        receiptNumber = "R" + this.pad(lastNo, 4);
                                    }
                                }

                                if (this.documentType == "Journal") {

                                    if (response.data.data.jvno == 0) {
                                        let lastNo = (new Date()).getFullYear() + "" + "0001"
                                        receiptNumber = "J" + lastNo;
                                    }
                                    else {
                                        let lastNo = (parseInt(response.data.data.jvno) + 1);
                                        receiptNumber = "J" + this.pad(lastNo, 4);
                                    }
                                }

                                if (this.documentType == "Payment") {
                                    if (response.data.data.paymentno == 0) {
                                        let lastNo = (new Date()).getFullYear() + "0001";
                                        receiptNumber = "P" + lastNo;
                                    }
                                    else {

                                        let lastNo = (parseInt(response.data.data.paymentno) + 1);
                                        receiptNumber = "P" + this.pad(lastNo, 4);
                                    }
                                }

                                this.documentNo = receiptNumber;
                            }
                        })
                        .catch(e => {
                            console.log(e)
                            this.errors.push(e)
                        });
                }
            
            },
            submitReceipt(e) {
                let postBody = {
                    documentNo : this.documentNo,
                    documentType: this.documentType,
                    referenceNo: this.referenceNo,
                    fundCode : this.fundtypecode,
                    documentDate: this.documentDate,
                    transactionsCR: this.items.filter(function (el) {
                        return el.creditAmount > 0
                            && el.debitAmount === 0
                    }),
                    transactionsDB: this.items.filter(function (el) {
                        return el.debitAmount > 0
                            && el.creditAmount === 0
                    })
                }
                //console.log(postBody, postBody.transactionsCR.length, postBody.transactionsDB.length);
                if ((!postBody.documentType || !postBody.documentNo  || !postBody.documentDate) || (!postBody.transactionsCR && !postBody.transactionsDB)) {
                    e.preventDefault();
                    alert("Supply the missing field(s)");
                    return;
                }
                else {
                    this.processingorsave = 'Processing';
                    e.disabled = true;
                    alert("processing")
                      axios.post(`/api/FinancialDoc/laodFinanCialDocument`, postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                          
                            if (response.data.responseCode == '200') {
                                alert('Posted Successfully');
                                window.location.reload()
                                 this.documentNo=''
                                 this.documentType=''
                                 this.referenceNo=''
                                 this.documentDate = ''
                                 this.items = []
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
                
            },
            cancel(item) {
                this.editIndex = null

                if (!this.originalData) {
                    this.items.splice(this.items.indexOf(item), 1)
                    return
                }

                Object.assign(item, this.originalData)
                this.originalData = null
            },

            remove(item, index) {
                 let chatofAccountItem = this.chartofAccountsStandBy.filter(function (el) {
                    return el.acctcode === item.code;
                })
                
                this.chartofAccountList.push(chatofAccountItem[0]);
                this.items.splice(index, 1)
            },

            save(item) {
                if (!item.code) {
                    alert('Supply the value for Account no');
                    return;
                }
                if(item.debitAmount < 0){
                  alert('Supply a Debit Ammount greater than Zero');
                    return;
                }

                if(item.creditAmount < 0){
                  alert('Supply a Credit Ammount greater than Zero');
                    return;
                }

                 if(item.creditAmount > 0 && item.debitAmount > 0){
                     item.debitAmount = 0
                     alert('Both Values cannot be used')
                    return;
                }

                if(item.creditAmount === 0 && item.debitAmount === 0){
                     item.debitAmount = 0
                     alert('Both Values cannot be Zero')
                    return;
                }

                if (item.code) {
                    this.chartofAccountsRemoved.push(item.code);
                    this.chartofAccountList = this.chartofAccountList.filter(function (el) {
                        return el.acctcode != item.code
                    })
                }
                this.originalData = null
                this.editIndex = null
            }
        }

    };
</script>